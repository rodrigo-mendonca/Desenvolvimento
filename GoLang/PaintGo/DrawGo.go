package main

import (
	_ "image/png"
	"log"
	"runtime"
	"github.com/go-gl/gl/v2.1/gl"
	"github.com/go-gl/glfw/v3.1/glfw"
    "math/rand"
    "time"
)

type Color struct {
    R, G ,B float32
}

func init() {
	runtime.LockOSThread()
    weight=400
    height=300
    rand.Seed( time.Now().UTC().UnixNano())
}

var weight int
var height int
var Matriz [][]Color


func main() {

    Matriz=make([][]Color,weight)

    for i := 0; i < weight; i++ {
        Matriz[i] = make([]Color,height)
    }
    
    for x := 0; x < weight; x++ {
        for y := 0; y < height; y++ {
            Matriz[x][y].R = randfloat();
            Matriz[x][y].G = randfloat();
            Matriz[x][y].B = randfloat();
        }
    }
    

	if err := glfw.Init(); err != nil {
		log.Fatalln("failed to initialize glfw:", err)
	}
	defer glfw.Terminate()

    // faz a configuracao da tela
	window, err := glfw.CreateWindow(weight, height, "TCC", nil, nil)
	if err != nil {
		panic(err)
	}
	window.MakeContextCurrent()
    // inicializa o Open GL
	if err := gl.Init(); err != nil {
		panic(err)
	}
    // faz a configuracao da cena
	setupScene()
    // desenha os pixels
    drawScene()
    // manda os pixels para a tela
    window.SwapBuffers()
    
	for !window.ShouldClose() {
		glfw.PollEvents()
	}
}

func setupScene() {
	gl.ClearColor(1, 1, 1, 0.0)

    gl.ClearDepth(1)
    gl.DepthFunc(gl.LEQUAL)

	gl.MatrixMode(gl.PROJECTION)
	gl.LoadIdentity()
	gl.Frustum(0, 133, 0, 100, 1.0, 10.0)
	gl.MatrixMode(gl.MODELVIEW)
	gl.LoadIdentity()
}

func destroyScene() {

}

func drawScene() {
    gl.Clear(gl.COLOR_BUFFER_BIT);

	gl.LoadIdentity()
	gl.Translatef(1, 0, -3)
	
    for x := 0; x < weight; x++ {
        for y := 0; y < height; y++ {
            gl.Color3f(Matriz[x][y].R, Matriz[x][y].G, Matriz[x][y].B)
            
            gl.Begin(gl.POINTS)
                gl.Vertex2i(int32(x), int32(y))
            gl.End()
        }
    }
}

func randfloat() float32 {
    return float32(rand.Float64()) 
}