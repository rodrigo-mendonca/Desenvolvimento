package somf

import (
	somk "../kohonen"
	somn "../neuron"
	"os"
    "os/exec"
    "gopkg.in/mgo.v2"
    "gopkg.in/mgo.v2/bson"
    "encoding/json"
    "strings"
    "strconv"
    "bufio"
    "fmt"
)

var Koh somk.Kohonen
var Server, Dbname, Trainname string
var Gridsize,Dimensions int
var Savetrain bool
var Loadtype int
var Filename string
var Error float64

func ShowPng(name string) {
    command := "open"
    arg1 := "-a"
    arg2 := "/Applications/Preview.app"
    cmd := exec.Command(command, arg1, arg2, name)
    err := cmd.Run()

    Checkerro(err)
}

func Checkerro(e error) {
    if e != nil {
        panic(e)
    }
}

func LoadColletion(name string) *mgo.Collection{
    // faz a conexao com a base de dados
    session, err := mgo.Dial(Server)
    if err != nil {
        panic(err)
    }

    session.SetMode(mgo.Monotonic, true)

    return session.DB(Dbname).C(name)
}

func LoadFile(f string) somk.Kohonen {
    // faz a leitura do arquivo
    file,err := os.Open(f)
    Checkerro(err)

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

            inputs := make([]float64,Dimensions)

            for i := 0; i < Dimensions; i++ {
                p:=params[i + 1]

                num,err:=strconv.ParseFloat(p, 64)

                inputs[i] = num
                Checkerro(err)
            }
            patterns = append(patterns, inputs)
        }
        numlines++;
    }

    Koh = Koh.Create(Gridsize,Dimensions)
    Koh.Labels   = labels
    Koh.Patterns = patterns
    Koh.Numlines = numlines
    return Koh
}

func LoadKDDCup() somk.Kohonen{
    //var patterns [][]float64
    var labels []string

    Colletion := LoadColletion("KDDCup")
    
    var listreg [][]string
    err := Colletion.Find(bson.M{}).All(&listreg)
    Checkerro(err)
    numlines:=0
    for _,reg:= range listreg{
        labels = append(labels, reg[0])
        
        fmt.Printf(reg[0]+"\n")

        numlines++
    }

    Koh = Koh.Create(Gridsize,Dimensions)
    Koh.Labels   = labels
    Koh.Numlines = numlines

    return Koh
}

func SaveTrainDB(){
    Colletion := LoadColletion("GridTrain")
    Colletion.RemoveAll(nil)

    ind:=0
    for _, newline:= range Koh.Outputs {
        for _, newreg:= range newline {
            err := Colletion.Insert(newreg)
            Checkerro(err)

            ind++
        }
    }

    fmt.Printf("Treinamento Salvo, Linhas: %d\n",ind)
}

func LoadTrainDB() [][]somn.Neuron{
    var DbTrain []somn.Neuron
    var ListTrain [][]somn.Neuron

    Colletion := LoadColletion(Trainname)

    err := Colletion.Find(bson.M{}).All(&DbTrain)
    Checkerro(err)

    return ListTrain
}

func SaveTrainJson(){
    o, err := os.Create("TrainDb.json")
    if err != nil {
        panic(err)
    }

    b, err := json.Marshal(Koh.Outputs)
    if err != nil {
        fmt.Println(err)
        return
    }
    o.WriteString(string(b))

    fmt.Printf("Treinamento Salvo\n")
}

func LoadTrainJson(){
    
}