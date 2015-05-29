#include <stdio.h>
#include <stdlib.h>
#include "PGM.h"
#include <limits.h>
#include <math.h>

void CalcFourier(PGM *);
void CalcParallelFourier(PGM *);
void CalcDistributedFourier(PGM *file);
void Fourier(INumber **,int,int,int);
INumber FourierFunc(INumber **,int,int,int,int,int,int);

double Espectro(INumber);
int Normalize(double,double);

int main(int argc, char *argv[])
{
    char *entrada = "imagem01.pgm";
    char *saida   = "imagem02.pgm";
    char *espectro= "espectro.pgm";
    char *tipo    = "I";

    if(argc > 1){
        entrada = argv[1];
        saida   = argv[2];
        tipo    = argv[3];
    }

    PGM *file = (PGM*)malloc(sizeof(PGM));

    readfile(entrada, file);

    switch (*tipo){
        case 'I':
            CalcFourier(file);
            break;
        case 'P':
            CalcParallelFourier(file);
            break;
    }

    PGM *fileesp = (PGM*)malloc(sizeof(PGM));
    fileesp->matrix = file->norespectro;

    savefile(espectro, fileesp);
    savefile(saida, file);

    free(file);

    printf("%s",saida);
    return 0;
}

void CalcFourier(PGM *file)
{
    //printf("Normal\n");
    int i,j;
    int w = file->width;
    int h = file->height;

    file->imatrix = (INumber**)alloc_Imatrix(h,w);

    for(i=0;i<h;i++)
        for(j=0;j<w;j++)
            file->imatrix[i][j].r = file->matrix[i][j];

    Fourier(file->imatrix,h,w,0);

    file->espectro = alloc_dmatrix(h,w);
    file->maxespectro = 0;

    for(i=0;i<h;i++)
        for(j=0;j<w;j++){
            file->espectro[i][j] = Espectro(file->imatrix[i][j]);

            if(file->maxespectro < file->espectro[i][j])
                file->maxespectro = file->espectro[i][j];

        }

    file->norespectro = alloc_matrix(h,w);
    for(i=0;i<h;i++)
        for(j=0;j<w;j++)
            file->norespectro[i][j] = Normalize(file->espectro[i][j],file->maxespectro);
}

void CalcParallelFourier(PGM *file)
{
    //printf("Paralelo\n");
    /*
    int i,j;
    int CHUNK = 100;
    int w = file->width;
    int h = file->height;
    int **matriz = alloc_matrix(h,w);
    #pragma omp parallel shared(file,matriz,w,h,CHUNK) private(i,j) num_threads(10)
    {
        #pragma omp for schedule(guided)
        for(i=0;i<h;i++)
        for(j=0;j<w;j++)
            matriz[i][j] = Fourier(file->matrix,i,j);
    }
    */
}
void Fourier(INumber **Matriz,int h,int w,int Inversa)
{
    int freq,i;

    for (i = 0; i < h; i++) {
        for (freq = 0; freq < w; freq++) {
            Matriz[i][freq] = FourierFunc(Matriz,i,freq,freq,h,1,Inversa);
        }
    }

    for (i = 0; i < w; i++) {
        for (freq = 0; freq < h; freq++) {
            Matriz[freq][i] = FourierFunc(Matriz,freq,i,freq,h,0,Inversa);
        }
    }
}

double Espectro(INumber var)
{
    return log(1 + sqrt(var.r*var.r + var.i*var.i));;
}

INumber FourierFunc(INumber **Matriz,int Y,int X,int Freq,int N,int Line,int Inversa)
{
    INumber Retorno;
    double re = 0;
    double im = 0;
    int t;

    int Neg = -1;
    if(Inversa)
        Neg = 1;


    for (t = 0; t < N; t++) {
        double time= (double)t;
        INumber var;

        // define se vai correr entre as linhas ou colulas
        if(Line)
            var = Matriz[Y][t];
        else
            var = Matriz[t][X];

        double rate = Neg * (2.0 * PI) * Freq * time / N;

        double re_part = var.r * cos(rate);
        double im_part = var.i * sin(rate);

        re += re_part;
        im += im_part;
    }

    if(Inversa){
        re = re / N;
        im = im / N;
    }

    Retorno.r = re;
    Retorno.i = im;

    return Retorno;
}

int Normalize(double Var,double Max)
{
    return (Var/Max)*255;
}
