package main

/*
Construa um parser para o arquivo de log games.log.
 
O arquivo games.log é gerado pelo servidor de quake 3 arena. 
Ele registra todas as informações dos jogos, quando um jogo começa, 
quando termina, quem matou quem, quem morreu pq caiu no vazio, 
quem morreu machucado, entre outros.

O parser deve ser capaz de ler o arquivo, agrupar os dados de cada 
jogo, e em cada jogo deve coletar as informações de morte.
 
Exemplo

21:42 Kill: 1022 2 22: <world> killed Isgalamido by MOD_TRIGGER_HURT
O player "Isgalamido" morreu pois estava ferido e caiu de uma 
altura que o matou.
 19 
2:22 Kill: 3 2 10: Isgalamido killed Dono da Bola by MOD_RAILGUN
O player "Isgalamido" matou o player Dono da Bola usando a 
arma Railgun.
 
Para cada jogo o parser deve gerar algo como:

game_1: {
	    total_kills: 45;
		players: ["Dono da bola", "Isgalamido", "Zeh"]
		kills: {
			"Dono da bola": 5,
			"Isgalamido": 18,
			"Zeh": 20
		}
	}
	Observações

	Quando o <world> mata o player ele perde -1 kill.
	<world> não é um player e não deve aparecer na lista de players e 
	nem no dicionário de kills.

	total_kills são os kills dos games, isso inclui mortes do <world>.
*/

import (
	"fmt"
	"os"
	"strings"
	"math"
	"strconv"
)

type Game struct{
	num int
	totalplayers int
	totalkills int
	listplayers []Player
}

type Player struct{
	id int
	name string
	numkill int
	
}

var listagame []Game
var totalgame int

func main() {
	totalgame = 0
	filename := "Quake.txt"
	// faz a leitura do arquivo
	file,err := os.Open(filename)
	checkerro(err)

	// faz a leitura da linha
	line:=readline(file)
	for line!="" {
		// remove o horario
		line = 	line[7:len(line)]
		scanline(line)
		// faz a leitura da proxima linha
		line=readline(file)
	}
	showstatic()
	fmt.Printf("Total de Jogos: %d",totalgame)
}

func checkerro(e error) {
    if e != nil {
        panic(e)
    }
}

func readline(f *os.File) string{
	ret :=""
	c := ""
	b := make([]byte, 1)

	// quan~to não for enter
	for c != "\n" {
		f.Read(b)

		// se não ler nada, arquivo acabou, então sai do loop
		if b[0] == 0{
			break
		}

		c = string(b)
		ret += c
	}
	return ret
}

func scanline(l string){
	i := strings.Index(l, ":")
	// se não tiver os :, ignora
	if i < 0{
		return
	}
	
	com := l[0:i]

	switch com {
		case "InitGame":
			scanGame()
		case "ClientUserinfoChanged":
			scanplayer(l[i+1:len(l)])
		case "Kill":
			scanKill(l[i+1:len(l)])
	}

}

func scanGame(){
	var newgame Game
	newgame.num = totalgame

	//fmt.Printf("Jogo:%d\n",totalgame)

	listagame = append(listagame, *newgame)
	totalgame = totalgame + 1
}

func scanplayer(l string){
	i := strings.Index(l, ":")+1
	j := strings.Index(l, "n\\")
	
	n:=strings.TrimSpace(l[i:j])
	
	id, _ := strconv.Atoi(n)

	i = strings.Index(l, "n\\")+2
	j = strings.Index(l, "t\\")-1
	name:= strings.TrimSpace(l[i:j])

	insertplayer(name,id)
}

func scanKill(l string){
	// remove as
	i := strings.Index(l, ":") + 2
	l = l[i:len(l)]

	tag:="killed"

	i = strings.Index(l, tag)
	j := strings.Index(l, "by")
	
	matou:= strings.TrimSpace(l[:i])
	morreu:= strings.TrimSpace(l[i+len(tag):j])
	//fmt.Printf("Ele %s Matou %s\n",matou,morreu)
	if matou == "<world>"{
		insertpoint(morreu,-1)
	} else {
		insertpoint(matou,1)
	}
}

func insertplayer(name string,id int){
	t:=totalgame-1
	p:=findplayer(id,listagame[t].listplayers)

	// se não encontrou, cria um novo player
	if p < 0 {
		var pla Player
		pla.id = id
		pla.name = name

		listagame[t].listplayers = append(listagame[t].listplayers, pla)

		listagame[t].totalplayers = len(listagame[t].listplayers)
		p = listagame[t].totalplayers-1
		//fmt.Printf("Posicao=%d Pontos=%d Player=%s \n",p,pla.numkill,name)
	}
	listagame[t].listplayers[p].name = name
}

func insertpoint(name string,pont int){
	t:=totalgame-1
	p:=findplayername(name,listagame[t].listplayers)

	// se não encontrou, cria um novo player
	if p >= 0 {
		listagame[t].listplayers[p].numkill+=pont
	}
	listagame[t].totalkills += int(math.Abs(float64(pont)))
}

func findplayer(p int,list []Player)int{
	ind:=-1
	
	for i := 0; i < len(list); i++ {
		if list[i].id == p {
			ind = i
			break
		}
	}
	return ind
}

func findplayername(p string,list []Player)int{
	ind:=-1
	
	for i := 0; i < len(list); i++ {
		if list[i].name == p {
			ind = i
			break
		}
	}
	return ind
}

func showstatic(){

	// open output file
    fo, err := os.Create("output.json")
    if err != nil {
        panic(err)
    }

	show:=""
	fo.WriteString("{")
	for i := 0; i < totalgame; i++ {
		game:=listagame[i]

		show = Aspas("game_"+strconv.Itoa(i+1))+":{\"total_kills\":"+Aspas(strconv.Itoa(game.totalkills))+",\"players\":["
		fo.WriteString(show)
		
		for j := 0; j < game.totalplayers; j++ {
			show =Aspas(game.listplayers[j].name)

			if(j < game.totalplayers-1){
				show += ","
			}
			fo.WriteString(show)
		}
		show ="],\"kills\":{"
		fo.WriteString(show)
		for j := 0; j < game.totalplayers; j++ {
			show =Aspas(game.listplayers[j].name)+":" + strconv.Itoa(game.listplayers[j].numkill)
			if(j < game.totalplayers-1){
				show += ","
			}
			fo.WriteString(show)
		}
		show ="}}\n"
		if(i < totalgame-1){
			show += ","
		}
		fo.WriteString(show)
	}
	fo.WriteString("}")
}

func Aspas(l string) string{
	return "\"" + l + "\""
}