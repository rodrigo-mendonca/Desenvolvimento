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

func main() {
    rand.Seed(time.Now().UTC().UnixNano())

    var k Kohonen

    k = k.Create(10,3)

    k.Exec("Food.txt")

    fmt.Printf("Concluido")
}

func checkerro(e error) {
    if e != nil {
        panic(e)
    }
}

// -----------------------------------Kohonen--------------------------------------------------------------------------------------
type Kohonen struct {
    outputs [][]Neuron
    iteration,length,dimenions int
    patterns [][]float64
    labels []string
}

func (r Kohonen) Create(l int, d int) Kohonen{
    r.length = l
    r.dimenions = d
    r.iteration = 0
    r.Initialise()
    return r
}

func (r Kohonen) Exec(f string) {
    r.LoadData(f)
    r.NormalisePatterns()
    r.Train(0.00000001)
    r.DumpCoordinates()
}

func (r Kohonen) Initialise() {
    r.outputs=make([][]Neuron,r.length)

    for i := 0; i < r.length; i++ {
        r.outputs[i] = make([]Neuron,r.length)
    }

    for i := 0; i < r.length; i++ {
        for j := 0; j < r.length; j++ {
            r.outputs[i][j].Create(i,j,r.length)
            r.outputs[i][j].Weigths = make([]float64,r.dimenions)

            for k := 0; k < r.dimenions; k++ {
                r.outputs[i][j].Weigths[k] = rand.Float64()
            }
        }
    }
}

func (r Kohonen) LoadData(f string) {

    // faz a leitura do arquivo
    file,err := os.Open(f)
    checkerro(err)

    reader := bufio.NewReader(file)
    scanner := bufio.NewScanner(reader)
    
    linenum:=0

    for scanner.Scan() {
        line:=scanner.Text()

        if(linenum>0){
            params:=strings.Split(line,",")
            
            r.labels=make([]string,r.dimenions+1)
            r.labels[0] = params[0]
            inputs := make([]float64,r.dimenions)

            for i := 0; i < r.dimenions; i++ {
                p:=params[i+1]

                r.labels[i+1] = p

                num,err:=strconv.ParseFloat(p, 64)

                inputs[i] = num

                checkerro(err)
            }
            r.patterns = append(r.patterns, inputs)
        }
        linenum++;
    }
}

func (r Kohonen) NormalisePatterns() {
    sum:=float64(0)
    l:=float64(len(r.patterns))

    for j := 0; j < r.dimenions; j++ {

        for _, num := range r.patterns {
            sum += num[j]
        }

        avg:= sum / l

        for i := 0; i < len(r.patterns); i++ {
            r.patterns[i][j] = r.patterns[i][j] / avg
        }
    }
}

func (r Kohonen) Train(maxErro float64) {
    erro:=maxErro

    for erro > maxErro {
        erro=0
        var TrainingSet [][]float64

        for _, num := range r.patterns {
            TrainingSet = append(TrainingSet,num)
        }

        for i := 0; i < len(r.patterns); i++ {
            ind:=rand.Intn(len(r.patterns) - i)
            pattern:=TrainingSet[ind]

            erro+=r.TrainPattern(pattern)

            TrainingSet = append(TrainingSet[:ind],TrainingSet[ind+1:]...)
        }
    }
}

func (r Kohonen) TrainPattern(pattern []float64) float64{
    erro:=float64(0)

    var winner Neuron

    for i := 0; i < r.length; i++ {
        for j := 0; j < r.length; j++ {
            erro+= r.outputs[i][j].UpdateWeigths(pattern,winner,r.iteration)
        }   
    }
    r.iteration++

    l:=float64(r.length)
    return math.Abs(erro / (l * l))
}

func (r Kohonen) DumpCoordinates() {

    for i := 0; i < len(r.patterns); i++ {
        neu:= r. Winner(r.patterns[i])
        fmt.Printf("%s %d %d\n",r.labels[i],neu.x,neu.y)
    }
}

func (r Kohonen) Winner(pattern []float64) Neuron{
    var winner Neuron

    min:=math.MaxFloat64

    for i := 0; i < r.length; i++ {
        for j := 0; j < r.length; j++ {
            dist:=r.Distance(pattern,r.outputs[i][j].Weigths)

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
    Weigths []float64
    RGB []int
    x,y,length int
    nf float64
}

func (r Neuron) Create(x int, y int, l int) {
    r.x = x
    r.y = y
    r.length = l

    dl:=float64(l)
    r.nf = 1000 / math.Log(dl)
}


func (r Neuron) Gauss(win Neuron, it int) float64 {
    dx:=float64(win.x - r.x)
    dy:=float64(win.y - r.y)

    distance:=math.Sqrt(math.Pow(dx, 2) + math.Pow(dy, 2))

    return math.Exp(-math.Pow(distance, 2) / math.Pow(r.Strength(it),2))
}

func (r Neuron) LearningRate(it int) float64 {
    dit:=float64(it)

    return math.Exp(-dit / 1000) * 0.1
}

func (r Neuron) Strength(it int) float64 {
    dit:=float64(it)
    dl:=float64(r.length)

    return math.Exp(-dit / r.nf) * dl
}

func (r Neuron) UpdateWeigths(pattern []float64,winner Neuron,it int) float64 {
    sum:=float64(0)

    for i := 0; i < r.length; i++ {
        delta:=r.LearningRate(it) * r.Gauss(winner, it) * (pattern[i] - r.Weigths[i])

        r.Weigths[i]+=delta

        //RGB[i] = 
        sum+=delta
    }
    dl:=float64(r.length)

    return sum / dl
}
// --------------------------------FimNeuronio--------------------------------------------------------------------------------------

