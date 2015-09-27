package main

import (
    "os"
	"math"
    "time"
    "fmt"
    "math/rand"
    "strings"
    "strconv"
    "bufio"
    "image"
    "image/color"
    "image/draw"
    "image/png"
    "log"
    "os/exec"
)

var (
    white color.Color = color.RGBA{255, 255, 255, 255}
    black color.Color = color.RGBA{0, 0, 0, 255}
    blue  color.Color = color.RGBA{0, 0, 255, 255}
)

func main() {
    rand.Seed(time.Now().UTC().UnixNano())

    var k Kohonen

    k = k.Create(400,3)
    k = k.Exec("Food.txt")

    Show(k.Before.Name())
    Show(k.After.Name())

    fmt.Printf("Concluido")
}

// ---------------------------------- Funcoes -------------------------------------------------------------------------------------

func checkerro(e error) {
    if e != nil {
        panic(e)
    }
}

func Show(name string) {
    command := "open"
    arg1 := "-a"
    arg2 := "/Applications/Preview.app"
    cmd := exec.Command(command, arg1, arg2, name)
    err := cmd.Run()
    if err != nil {
        log.Fatal(err)
    }
}

// ---------------------------------- FimFuncoes ----------------------------------------------------------------------------------


// -----------------------------------Kohonen--------------------------------------------------------------------------------------
type Kohonen struct {
    outputs [][]Neuron
    iteration,length,dimenions,numlines int
    patterns [][]float64
    labels []string
    Before *os.File
    After *os.File

}

func (r Kohonen) Create(l int, d int) Kohonen{
    r.length = l
    r.dimenions = d
    r.iteration = 0
    r.Before, _ = os.Create("Before.png")
    r.After, _ = os.Create("After.png")

    r = r.Initialise()

    return r
}

func (r Kohonen) Exec(f string) Kohonen{
    r = r.LoadData(f)
    r = r.NormalisePatterns()
    r = r.Train(0.0000001)
    
    Screen := image.NewRGBA(image.Rect(0, 0, r.length, r.length))
    draw.Draw(Screen, Screen.Bounds(), &image.Uniform{white}, image.ZP, draw.Src)

    for i := 0; i < r.length; i++ {
        for j := 0; j < r.length; j++ {
            red:=uint8(r.outputs[i][j].RGB[0])
            green:=uint8(r.outputs[i][j].RGB[1])
            blue:=uint8(r.outputs[i][j].RGB[2])

            Screen.Set(i, j, color.RGBA{red, green, blue, 255})
        }
    }
    png.Encode(r.After, Screen)

    r.DumpCoordinates()

    return r
}

func (r Kohonen) Initialise() Kohonen{
    r.outputs=make([][]Neuron,r.length)

    for i := 0; i < r.length; i++ {
        r.outputs[i] = make([]Neuron,r.length)
    }

    Screen := image.NewRGBA(image.Rect(0, 0, r.length, r.length))
    draw.Draw(Screen, Screen.Bounds(), &image.Uniform{white}, image.ZP, draw.Src)

    for i := 0; i < r.length; i++ {
        for j := 0; j < r.length; j++ {
            r.outputs[i][j] = r.outputs[i][j].Create(i,j,r.length)
            r.outputs[i][j].Weights = make([]float64,r.dimenions)
            r.outputs[i][j].RGB = make([]int,r.dimenions)

            for k := 0; k < r.dimenions; k++ {
                r.outputs[i][j].Weights[k] = rand.Float64()
                r.outputs[i][j].RGB[k] = int((r.outputs[i][j].Weights[k] * 255))
            }

            red:=uint8(r.outputs[i][j].RGB[0])
            green:=uint8(r.outputs[i][j].RGB[1])
            blue:=uint8(r.outputs[i][j].RGB[2])

            Screen.Set(i, j, color.RGBA{red, green, blue, 255})
        }
    }
    
    png.Encode(r.Before, Screen)

    return r
}

func (r Kohonen) LoadData(f string) Kohonen {
    // faz a leitura do arquivo
    file,err := os.Open(f)
    checkerro(err)

    reader := bufio.NewReader(file)
    scanner := bufio.NewScanner(reader)
    
    r.numlines=-1

    for scanner.Scan() {
        line:=scanner.Text()

        if(r.numlines>-1){
            params:=strings.Split(line,",")
            
            r.labels = append(r.labels, params[0])

            inputs := make([]float64,r.dimenions)

            for i := 0; i < r.dimenions; i++ {
                p:=params[i + 1]

                num,err:=strconv.ParseFloat(p, 64)

                inputs[i] = num
                checkerro(err)
            }
            r.patterns = append(r.patterns, inputs)
        }
        r.numlines++;
    }

    return r
}

func (r Kohonen) NormalisePatterns() Kohonen{
    sum:=float64(0)
    l:=float64(len(r.patterns))

    for j := 0; j < r.dimenions; j++ {

        for _, num := range r.patterns {
            sum += num[j]
        }

        avg:= sum / l

        for i := 0; i < r.numlines; i++ {
            r.patterns[i][j] = r.patterns[i][j] / avg
        }
    }
    return r
}

func (r Kohonen) Train(maxErro float64) Kohonen{
    erro:=math.MaxFloat64

    for erro > maxErro {
        erro=float64(0)

        var TrainingSet [][]float64

        for _, num := range r.patterns {
            TrainingSet = append(TrainingSet,num)
        }

        for i := 0; i < r.numlines; i++ {
            ind:=rand.Intn((r.numlines - i))

            pattern:=TrainingSet[ind]
            v:=float64(0)
            v,r = r.TrainPattern(pattern)
            erro+=v
            
            TrainingSet = append(TrainingSet[:ind],TrainingSet[ind+1:]...)
        }
        //fmt.Printf("Erro:%f\n",erro)
    }

    return r
}

func (r Kohonen) TrainPattern(pattern []float64) (float64,Kohonen){
    erro:=float64(0)

    var winner Neuron
    winner = r.Winner(pattern)

    for i := 0; i < r.length; i++ {
        for j := 0; j < r.length; j++ {
            v:=float64(0)
            v,r.outputs[i][j] = r.outputs[i][j].UpdateWeigths(pattern,winner,r.iteration)
            erro+= v
        }   
    }
    r.iteration++
    
    l:=float64(r.length)
    return math.Abs(erro / (l * l)),r
}

func (r Kohonen) DumpCoordinates(){
    i:=0
    for _, num := range r.patterns {
        neu:= r. Winner(num)
        
        s:=r.labels[i]
        fmt.Printf("%s,%d,%d\n",s,neu.x,neu.y)
        i++
    }
}

func (r Kohonen) Winner(pattern []float64) Neuron{
    var winner Neuron

    min:=math.MaxFloat64

    for i := 0; i < r.length; i++ {
        for j := 0; j < r.length; j++ {
            dist:=r.Distance(pattern,r.outputs[i][j].Weights)

            if(dist< min){
                min = dist
                
                winner = r.outputs[i][j]
            }
        }
    }

    return winner
}

func (r Kohonen) Distance(v1 []float64,v2 []float64)  float64{
    v:=float64(0)

    for i := 0; i < len(v1); i++ {
        v+=math.Pow(v1[i] -v2[i],2)
    }

    return math.Sqrt(v)
}
// -----------------------------------FimKohonen-----------------------------------------------------------------------------------


// -----------------------------------Neuronio-------------------------------------------------------------------------------------
type Neuron struct {
    Weights []float64
    RGB []int
    x,y,length int
    nf float64
    maxint float64
    maxvar float64
}

func (r Neuron) Create(x int, y int, l int) Neuron{
    r.x = x
    r.y = y
    r.length = l
    r.maxint = 1000
    r.maxvar = 0.1

    dl:=float64(l)

    log:=math.Log(dl)
    r.nf = r.maxint / log
    
    return r
}


func (r Neuron) Gauss(win Neuron, it int) float64 {
    dx:=float64(win.x - r.x)
    dy:=float64(win.y - r.y)

    distance:=math.Sqrt(math.Pow(dx, 2) + math.Pow(dy, 2))

    return math.Exp(-math.Pow(distance, 2) / math.Pow(r.Strength(it),2))
}

func (r Neuron) LearningRate(it int) float64 {
    dit:=float64(it)

    return math.Exp(-dit / r.maxint) * r.maxvar // 1000 tem que ser constrante
}

func (r Neuron) Strength(it int) float64 {
    dit:=float64(it)
    dl:=float64(r.length)
    return math.Exp(-dit / r.nf) * dl
}

func (r Neuron) UpdateWeigths(pattern []float64,winner Neuron,it int) (float64,Neuron) {
    sum:=float64(0)
    
    for i := 0; i < len(r.Weights); i++ {
        delta:=r.LearningRate(it) * r.Gauss(winner, it) * (pattern[i] - r.Weights[i])

        r.Weights[i]+=delta

        r.RGB[i] = int((r.Weights[i] * 255))

        if r.RGB[i] > 255{
            r.RGB[i] = 255
        }

        sum+=delta
    }

    dl:=float64(r.length)

    return sum / dl, r
}
// --------------------------------FimNeuronio--------------------------------------------------------------------------------------

