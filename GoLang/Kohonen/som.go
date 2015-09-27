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
)

type Color struct {
    R, G ,B float32
}

func main() {
    rand.Seed(time.Now().UTC().UnixNano())

    var k Kohonen

    k = k.Create(10,3)

    k.Exec("Food.txt")

    fmt.Printf("Concluido")
}

// ---------------------------------- Funcoes -------------------------------------------------------------------------------------
func checkerro(e error) {
    if e != nil {
        panic(e)
    }
}
// ---------------------------------- FimFuncoes ----------------------------------------------------------------------------------


// -----------------------------------Kohonen--------------------------------------------------------------------------------------
type Kohonen struct {
    outputs [][]Neuron
    iteration,length,dimenions,numlines int
    patterns [][]float64
    labels []string
    Colors [][]Color
}

func (r Kohonen) Create(l int, d int) Kohonen{
    r.length = l
    r.dimenions = d
    r.iteration = 0
    r = r.Initialise()
    return r
}

func (r Kohonen) Exec(f string) {
    r = r.LoadData(f)
    r = r.NormalisePatterns()
    r = r.Train(0.0000001)
    r.DumpCoordinates()
}

func (r Kohonen) Initialise() Kohonen{
    r.outputs=make([][]Neuron,r.length)

    for i := 0; i < r.length; i++ {
        r.outputs[i] = make([]Neuron,r.length)
    }

    for i := 0; i < r.length; i++ {
        for j := 0; j < r.length; j++ {
            r.outputs[i][j] = r.outputs[i][j].Create(i,j,r.length)
            r.outputs[i][j].Weights = make([]float64,r.dimenions)
            r.outputs[i][j].RGB = make([]int,r.dimenions)

            for k := 0; k < r.dimenions; k++ {
                r.outputs[i][j].Weights[k] = rand.Float64()
            }
        }
    }
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
}

func (r Neuron) Create(x int, y int, l int) Neuron{
    r.x = x
    r.y = y
    r.length = l

    dl:=float64(l)

    log:=math.Log(dl)
    r.nf = 1000 / log
    
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

    return math.Exp(-dit / 1000) * 0.1 // 1000 tem que ser constrante
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

