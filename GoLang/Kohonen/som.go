package main

import (
    "os"
	"math"
    "time"
    "fmt"
    "flag"
    "math/rand"
    "strings"
    "strconv"
    "bufio"
    "image"
    "image/color"
    "image/draw"
    "image/png"
    "os/exec"
    "gopkg.in/mgo.v2"
    "gopkg.in/mgo.v2/bson"
    "encoding/json"
)

var (
    white color.Color = color.RGBA{255, 255, 255, 255}
    black color.Color = color.RGBA{0, 0, 0, 255}

    red  color.Color = color.RGBA{255, 0, 0, 255}
    green  color.Color = color.RGBA{0, 255, 0, 255}
    blue  color.Color = color.RGBA{0, 0, 255, 255}
)

type KDDCup struct{
    Duration int
    Protocol_type string
    Service string
    Flag string
    Src_bytes int
    Dst_bytes int
    Land string
    Wrong_fragment int
    Urgent int
    Hot int
    Num_failed_logins int
    Logged_in string
    Num_compromised int
    Root_shell int
    Su_attempted int 
    Num_root int
    Num_file_creations int
    Num_shells int
    Num_access_files int
    Num_outbound_cmds int
    Is_host_login string
    Is_guest_login string
    Count int
    Srv_count int
    Serror_rate int
    Srv_serror_rate int
    Rerror_rate int
    Srv_rerror_rate int
    Same_srv_rate int
    Diff_srv_rate int
    Srv_diff_host_rate int
    Dst_host_count int
    Dst_host_srv_count int
    Dst_host_same_srv_rate int
    Dst_host_diff_srv_rate int
    Dst_host_same_src_port_rate int
    Dst_host_srv_diff_host_rate int
    Dst_host_serror_rate int
    Dst_host_srv_serror_rate int
    Dst_host_rerror_rate int
    Dst_host_srv_rerror_rate int
    Attack string
}

var k Kohonen
var Gridsize int
var Server, TrainName string
var SaveTrain bool

func main() {
    LoadParams()

    rand.Seed(time.Now().UTC().UnixNano())

    loadtype:=0

    // faz a leitura dos dados de treinamento
    if loadtype == 0 {
        k = LoadData("Food.txt",3)
    }
    if loadtype == 1 {
        k = LoadKDDCup(42)
    }

    // faz o treinamento da base de dados
    k = k.Train(0.0000001)
    // Desenha o estado atual da grade
    k.Draw()

    k.DumpCoordinates()

    //ShowPng(k.Before.Name())
    //ShowPng(k.After.Name())

    if SaveTrain{
        SaveTrainJson()
    }
    fmt.Printf("Concluido!")
}

// ---------------------------------- Funcoes -------------------------------------------------------------------------------------

func checkerro(e error) {
    if e != nil {
        panic(e)
    }
}

func LoadParams(){
    fmt.Println("Params")
    flag.StringVar(&Server,"server", "localhost", "Server name")
    flag.IntVar(&Gridsize,"grid", 10, "Grid Size")
    flag.BoolVar(&SaveTrain,"s", false, "Training Save?")
    flag.StringVar(&TrainName,"train", "GridTrain", "Training name")

    config:= flag.String("config", "", "Config file")

    flag.Parse()

    fmt.Println("-Server:", Server)
    fmt.Println("-Grid Size:", Gridsize)
    fmt.Println("-Training Save?:", SaveTrain)

    if SaveTrain{
        fmt.Println("-Training name:", TrainName)
    }

    fmt.Println("-Config:", *config)

    if *config!="" {
        file,err := os.Open(*config)
        checkerro(err)

        reader := bufio.NewReader(file)
        scanner := bufio.NewScanner(reader)
        for scanner.Scan() {
            line:=scanner.Text()
            fmt.Println("--"+line)
        }
    }
    fmt.Println("Traning...")
}

func ShowPng(name string) {
    command := "open"
    arg1 := "-a"
    arg2 := "/Applications/Preview.app"
    cmd := exec.Command(command, arg1, arg2, name)
    err := cmd.Run()

    checkerro(err)
}

func LoadColletion(name string) *mgo.Collection{
    // faz a conexao com a base de dados
    session, err := mgo.Dial(Server)
    if err != nil {
        panic(err)
    }

    session.SetMode(mgo.Monotonic, true)

    return session.DB("TCC").C(name)
}

func LoadData(f string,dimensions int) Kohonen {
    // faz a leitura do arquivo
    file,err := os.Open(f)
    checkerro(err)

    reader := bufio.NewReader(file)
    scanner := bufio.NewScanner(reader)
    
    numlines:=-1
    var patterns [][]float64
    var labels []string

    for scanner.Scan() {
        line:=scanner.Text()

        if(numlines>-1){
            params:=strings.Split(line,",")
            
            labels = append(labels, params[0])
            //fmt.Printf("%s,",params[0])
            inputs := make([]float64,dimensions)

            for i := 0; i < dimensions; i++ {
                p:=params[i + 1]
                
                num,err:=strconv.ParseFloat(p, 64)
                //fmt.Printf("%f,",num)
                inputs[i] = num
                checkerro(err)
            }
            //fmt.Printf("\n")
            patterns = append(patterns, inputs)
        }
        numlines++;
    }

    k = k.Create(Gridsize,dimensions)
    k.labels   = labels
    k.patterns = patterns
    k.numlines = numlines

    return k
}

func LoadKDDCup(dimensions int) Kohonen{

    //var patterns [][]float64
    var labels []string

    Colletion := LoadColletion("KDDCup")
    
    var listreg [][]string
    err := Colletion.Find(bson.M{}).All(&listreg)
    checkerro(err)
    numlines:=0
    for _,reg:= range listreg{
        labels = append(labels, reg[0])
        
        fmt.Printf(reg[0]+"\n")

        numlines++
    }

    k = k.Create(Gridsize,dimensions)
    k.labels   = labels
    k.numlines = numlines

    return k
}

func SaveTrainDB(){
    Colletion := LoadColletion("GridTrain")
    Colletion.RemoveAll(nil)

    ind:=0
    for _, newline:= range k.outputs {
        for _, newreg:= range newline {
            err := Colletion.Insert(newreg)
            checkerro(err)

            ind++
        }
    }

    fmt.Printf("Treinamento Salvo, Linhas: %d\n",ind)
}

func LoadTrainDB() [][]Neuron{
    var DbTrain []Neuron
    var ListTrain [][]Neuron

    Colletion := LoadColletion(TrainName)

    err := Colletion.Find(bson.M{}).All(&DbTrain)
    checkerro(err)

    return ListTrain
}

func SaveTrainJson(){
    o, err := os.Create("TrainDb.json")
    if err != nil {
        panic(err)
    }

    b, err := json.Marshal(k.outputs)
    if err != nil {
        fmt.Println(err)
        return
    }
    o.WriteString(string(b))

    fmt.Printf("Treinamento Salvo\n")
}

func LoadTrainJson(){
    
}
// ---------------------------------- FimFuncoes ----------------------------------------------------------------------------------


// -----------------------------------Kohonen--------------------------------------------------------------------------------------
type Kohonen struct {
    outputs [][]Neuron
    iteration,length,dimensions,numlines int
    patterns [][]float64
    labels []string
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
            red:=uint8(r.outputs[i][j].RGB[0])
            green:=uint8(r.outputs[i][j].RGB[1])
            blue:=uint8(r.outputs[i][j].RGB[2])

            Screen.Set(i, j, color.RGBA{red, green, blue, 255})
        }
    }
    png.Encode(r.After, Screen)
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
            r.outputs[i][j].Weights = make([]float64,r.dimensions)
            r.outputs[i][j].RGB = make([]int,r.dimensions)

            for k := 0; k < r.dimensions; k++ {
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

func (r Kohonen) NormalisePatterns() Kohonen{
    sum:=float64(0)
    l:=float64(len(r.patterns))

    for j := 0; j < r.dimensions; j++ {

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

    r = r.NormalisePatterns()

    erro:=math.MaxFloat64

    for erro > maxErro {
        erro=float64(0)

        var TrainingSet [][]float64

        for _, num := range r.patterns {
            TrainingSet = append(TrainingSet,num)
            fmt.Printf("%v\n", num)
        }
        fmt.Printf("\n")
        
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
        neu:= r.Winner(num)
        
        s:=r.labels[i]
        fmt.Printf("%s,%d,%d\n",s,neu.X,neu.Y)
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
    X,Y,Length int
    Nf float64
    Maxint float64
    Maxvar float64
    Weights []float64
    RGB []int
}

func (r Neuron) Create(x int, y int, l int) Neuron{
    r.X = x
    r.Y = y
    r.Length = l
    r.Maxint = float64(1000)
    r.Maxvar = float64(0.1)

    dl:=float64(l)

    log:=math.Log(dl)
    r.Nf = r.Maxint / log
    
    return r
}


func (r Neuron) Gauss(win Neuron, it int) float64 {
    dx:=float64(win.X - r.X)
    dy:=float64(win.X - r.Y)

    distance:=math.Sqrt(math.Pow(dx, 2) + math.Pow(dy, 2))

    return math.Exp(-math.Pow(distance, 2) / math.Pow(r.Strength(it),2))
}

func (r Neuron) LearningRate(it int) float64 {
    dit:=float64(it)

    return math.Exp(-dit / r.Maxint) * r.Maxvar // 1000 tem que ser constrante
}

func (r Neuron) Strength(it int) float64 {
    dit:=float64(it)
    dl:=float64(r.Length)
    return math.Exp(-dit / r.Nf) * dl
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

    dl:=float64(r.Length)

    return sum / dl, r
}
// --------------------------------FimNeuronio--------------------------------------------------------------------------------------