package kohonen

import (
    neu "../neuron"
    "os"
    "fmt"
    "math"
    "math/rand"
    "image"
    "image/color"
    "image/draw"
    "image/png"
)

var (
    white color.Color = color.RGBA{255, 255, 255, 255}
    black color.Color = color.RGBA{0, 0, 0, 255}

    red  color.Color = color.RGBA{255, 0, 0, 255}
    green  color.Color = color.RGBA{0, 255, 0, 255}
    blue  color.Color = color.RGBA{0, 0, 255, 255}
)

type Kohonen struct {
    Outputs [][]neu.Neuron
    iteration,length,dimensions,Numlines int
    Patterns [][]float64
    Labels []string
    Before *os.File
    After *os.File
}

func (r Kohonen) Create(l int, d int) Kohonen{
    r.length = l
    r.dimensions = d
    r.iteration = 0
    r.Before, _ = os.Create("Before.png")
    r.After, _ = os.Create("After.png")

    r = r.Initialise()

    return r
}

func (r Kohonen) Draw(){
    Screen := image.NewRGBA(image.Rect(0, 0, r.length, r.length))
    draw.Draw(Screen, Screen.Bounds(), &image.Uniform{white}, image.ZP, draw.Src)

    for i := 0; i < r.length; i++ {
        for j := 0; j < r.length; j++ {
            red:=uint8(r.Outputs[i][j].RGB[0])
            green:=uint8(r.Outputs[i][j].RGB[1])
            blue:=uint8(r.Outputs[i][j].RGB[2])

            Screen.Set(i, j, color.RGBA{red, green, blue, 255})
        }
    }
    png.Encode(r.After, Screen)
}

func (r Kohonen) Initialise() Kohonen{
    r.Outputs=make([][]neu.Neuron,r.length)

    for i := 0; i < r.length; i++ {
        r.Outputs[i] = make([]neu.Neuron,r.length)
    }

    Screen := image.NewRGBA(image.Rect(0, 0, r.length, r.length))
    draw.Draw(Screen, Screen.Bounds(), &image.Uniform{white}, image.ZP, draw.Src)

    for i := 0; i < r.length; i++ {
        for j := 0; j < r.length; j++ {
            r.Outputs[i][j] = r.Outputs[i][j].Create(i,j,r.length)
            r.Outputs[i][j].Weights = make([]float64,r.dimensions)
            r.Outputs[i][j].RGB = make([]int,r.dimensions)

            for k := 0; k < r.dimensions; k++ {
                r.Outputs[i][j].Weights[k] = rand.Float64()
                r.Outputs[i][j].RGB[k] = int((r.Outputs[i][j].Weights[k] * 255))
            }

            red:=uint8(r.Outputs[i][j].RGB[0])
            green:=uint8(r.Outputs[i][j].RGB[1])
            blue:=uint8(r.Outputs[i][j].RGB[2])

            Screen.Set(i, j, color.RGBA{red, green, blue, 255})
        }
    }
    
    png.Encode(r.Before, Screen)

    return r
}

func (r Kohonen) NormalisePatterns() Kohonen{
    sum:=float64(0)
    l:=float64(len(r.Patterns))

    for j := 0; j < r.dimensions; j++ {

        for _, num := range r.Patterns {
            sum += num[j]
        }

        avg:= sum / l

        for i := 0; i < r.Numlines; i++ {
            r.Patterns[i][j] = r.Patterns[i][j] / avg
        }
    }
    return r
}

func (r Kohonen) Train(maxErro float64) Kohonen{
    r = r.NormalisePatterns()
    erro:=math.MaxFloat64

    for erro > maxErro {
        erro=float64(0)

        var TrainingSet [][]float64

        for _, num := range r.Patterns {
            TrainingSet = append(TrainingSet,num)
        }

        for i := 0; i < r.Numlines; i++ {
            ind:=rand.Intn((r.Numlines - i))

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

    var winner neu.Neuron
    winner = r.Winner(pattern)

    for i := 0; i < r.length; i++ {
        for j := 0; j < r.length; j++ {
            v:=float64(0)
            v,r.Outputs[i][j] = r.Outputs[i][j].UpdateWeigths(pattern,winner,r.iteration)
            erro+= v
        }   
    }
    r.iteration++
    
    l:=float64(r.length)
    return math.Abs(erro / (l * l)),r
}

func (r Kohonen) DumpCoordinates(){
    i:=0
    for _, num := range r.Patterns {
        neu:= r. Winner(num)
        
        s:=r.Labels[i]
        fmt.Printf("%s,%d,%d\n",s,neu.X,neu.Y)
        i++
    }
}

func (r Kohonen) Winner(pattern []float64) neu.Neuron{
    var winner neu.Neuron

    min:=math.MaxFloat64

    for i := 0; i < r.length; i++ {
        for j := 0; j < r.length; j++ {
            dist:=r.Distance(pattern,r.Outputs[i][j].Weights)

            if(dist< min){
                min = dist
                
                winner = r.Outputs[i][j]
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