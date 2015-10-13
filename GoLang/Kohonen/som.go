package main

import (
    //"./somstructs"
    somf "./somfunctions"
    "fmt"
    "flag"
    
    "os"
    "bufio"
)

func main() {
    LoadParams()

    // faz a leitura dos dados de treinamento
    if somf.Loadtype == 0 {
        somf.Koh = somf.LoadFile(somf.Filename)
    }
    if somf.Loadtype == 1 {
        somf.Koh = somf.LoadKDDCup()
    }
    if somf.Loadtype == 2 {
        //Koh = LoadJson()
    }

    // faz o treinamento da base de dados
    somf.Koh = somf.Koh.Train(5000)
    // Desenha o estado atual da grade
    somf.Koh.Draw()

    if somf.Savetrain{
        somf.SaveTrainJson()
    }
    
    var num float64
    
    for true {
        var a []float64
        a = append(a, 0,0,0)
        for i := 0; i < 3; i++ {
            fmt.Printf("%d >> ", i)
            n, err := fmt.Scanf("%g\n", &num)
            if err != nil {
                fmt.Println(n, err)
            }
            a[i] = num
        }
        
        somf.Koh.Test(a)
    }
    
    fmt.Printf("Completed!")
}

func LoadParams(){
    fmt.Println("Params")
    flag.StringVar(&somf.Server,"server", "localhost", "Server name")
    flag.StringVar(&somf.Dbname,"base", "TCC", "Data base name")
    flag.IntVar(&somf.Gridsize,"grid", 10, "Grid Size")
    flag.BoolVar(&somf.Savetrain,"s", false, "Training Save?")
    flag.StringVar(&somf.Trainname,"train", "GridTrain", "Training name")
    flag.IntVar(&somf.Loadtype,"type", 0, "0-Load file,1-Load KddCup")
    flag.StringVar(&somf.Filename,"f", "", "File name")
    flag.IntVar(&somf.Dimensions,"dim", 3, "Dimensions Weigths")
    flag.Float64Var(&somf.Error,"err", 0.0000001, "Minimum error")

    config:= flag.String("config", "", "Config file")

    flag.Parse()
    
    fmt.Println("-type:", somf.Loadtype)

    if somf.Loadtype == 0{
        fmt.Println("-File:", somf.Filename)
    }

    if somf.Loadtype == 1{
        fmt.Println("-Server:", somf.Server)
        fmt.Println("-DataBase:", somf.Server)
    }

    fmt.Println("-Grid Size:", somf.Gridsize)
    fmt.Println("-Training Save?:", somf.Savetrain)

    if somf.Savetrain{
        fmt.Println("-Training name:", somf.Trainname)
    }

    if *config!="" {
        fmt.Println("-Config:", *config)
        file,err := os.Open(*config)
        somf.Checkerro(err)

        reader := bufio.NewReader(file)
        scanner := bufio.NewScanner(reader)
        for scanner.Scan() {
            line:=scanner.Text()
            fmt.Println("--"+line)
        }
    }
    fmt.Println("Trainning...")
}