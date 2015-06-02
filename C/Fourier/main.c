#include <stdio.h>
#include <stdlib.h>
#include "PGM.h"
#include <limits.h>
#include <math.h>

void CalcFourier(PGM *);
void CalcParallelFourier(PGM *);
void CalcDistributedFourier(PGM *file);
void Fourier(INumber **,INumber **,int,int,int);
INumber FourierFunc(INumber **,int,int,int,int,int,int);

double Espectro(INumber);
int Normalize(double,double);

int main(int argc, char *argv[])
{
    char *entrada = "imagem04.pgm";
    char *original = "original04.pgm";
    char *saida   = "Inversa04.pgm";
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
    fileesp->height = file->height;
    fileesp->width = file->width;
    fileesp->maxGray = file->maxGray;
    fileesp->matrix = file->norespectro;

    PGM *fileinv = (PGM*)malloc(sizeof(PGM));
    fileinv->height = file->height;
    fileinv->width = file->width;
    fileinv->maxGray = file->maxGray;
    fileinv->matrix = file->matrixinv;

    savefile(espectro, fileesp);
    savefile(original, file);
    savefile(saida, fileinv);

    free(fileesp);
    free(file);
    free(fileinv);

    printf("%s",saida);
    return 0;
}

void CalcFourier(PGM *file)
{
    int i,j;
    int w = file->width;
    int h = file->height;

    INumber** imatrix = (INumber**)alloc_Imatrix(h,w);
    file->imatrix     = (INumber**)alloc_Imatrix(h,w);

    for(i=0;i<h;i++)
        for(j=0;j<w;j++){
            imatrix[i][j].r = (double)file->matrix[i][j];
            imatrix[i][j].i = 0;

            file->imatrix[i][j].r = 0;
            file->imatrix[i][j].i = 0;
        }

    // calculo do fourier
    Fourier(file->imatrix,imatrix,h,w,0);

    // CALCULO DO ESPECTRO
    file->espectro = alloc_dmatrix(h,w);
    file->maxespectro = 0;

    for(i=0;i<h;i++){
        for(j=0;j<w;j++){
            printf("i:%d,j:%d - ",i,j);
            file->espectro[i][j] = Espectro(file->imatrix[i][j]);

            if(j % 100 == 0)
                system("pause");

            if(file->maxespectro < file->espectro[i][j])
                file->maxespectro = file->espectro[i][j];
        }
    }

    file->norespectro = alloc_matrix(h,w);
    for(i=0;i<h;i++)
        for(j=0;j<w;j++)
            file->norespectro[i][j] = Normalize(file->espectro[i][j],file->maxespectro);


    file->imatrixinv =(INumber**)alloc_Imatrix(h,w);
    Fourier(imatrix,file->imatrixinv,h,w,1);

    file->matrixinv = (int**)alloc_matrix(h, w);

    for(i=0;i<h;i++)
        for(j=0;j<w;j++){
            file->matrixinv[i][j] = file->imatrixinv[i][j].r;
        }
}

void CalcParallelFourier(PGM *file)
{

}

void Fourier(INumber **iMatriz,INumber **Matriz,int h,int w,int Inversa)
{
    int freq,i;

    for (i = 0; i < h; i++) {
        for (freq = 0; freq < w; freq++) {
            iMatriz[i][freq] = FourierFunc(Matriz,i,freq,freq,w,1,Inversa);
        }
    }

    for (i = 0; i < w; i++) {
        for (freq = 0; freq < h; freq++) {
            Matriz[freq][i] = FourierFunc(iMatriz,freq,i,freq,h,0,Inversa);
        }
    }

    iMatriz = Matriz;
}

double Espectro(INumber var)
{
    long double a = var.r*var.r;
    long double b = var.i*var.i;
    double T = sqrt(a + b);
    double nlog = log(1 + T);

    return nlog;
}

INumber FourierFunc(INumber **Matriz,int Y,int X,int Freq,int N,int Line,int Inversa)
{
    INumber Retorno;
    long double re = 0;
    long double im = 0;
    int t;
    double Nt = (double)N;

    int Neg = -1;
    if(Inversa)
        Neg = 1;

    for (t = 0; t < N; t++) {
        double time = (double)t;
        double Fr = (double)Freq;
        INumber var;

        // define se vai correr entre as linhas ou colulas
        if(Line){
            var = Matriz[Y][t];
        }
        else{
            var = Matriz[t][X];
        }

        double rate = Neg * (2.0 * PI) * Fr * time / Nt;

        INumber Part;
        Part.r = cos(rate);
        Part.i = sin(rate);

        INumber Result = IMult(var,Part);

        re += Result.r;
        im += Result.i;
    }

    if(Inversa){
        re = re / Nt;
        im = im / Nt;
    }

    Retorno.r = re;
    Retorno.i = im;

    return Retorno;
}

int Normalize(double Var,double Max)
{
    double N = Max;
    int retorno = (int)((Var/N)*255);

    if(retorno > 255)
        retorno = 255;

    if(retorno < 0)
        retorno = 0;


    printf("Var:%g,N:%g = %d\n",Var,N,retorno);

    return retorno;
}
