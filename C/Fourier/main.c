#include <stdio.h>
#include <stdlib.h>
#include "PGM.h"
#include <limits.h>
#include <omp.h>
#include <math.h>

void CalcFourier(PGM *);
void CalcParallelFourier(PGM *);
void CalcDistributedFourier(PGM *file);
INumber *Fourier(int *,int,int,int);
int Espectro(INumber);

int main(int argc, char *argv[])
{
    char *entrada = "imagem01.pgm";
    char *saida   = "imagem02.pgm";
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
    savefile(saida, file);
    free(file);

    printf(saida);
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
        file->imatrix[i] = Fourier(file->matrix[i],i,w,0);

    file->espectro = alloc_matrix(h,w);

    for(i=0;i<h;i++)
        for(j=0;j<w;j++)
            file->espectro[i][j] = Espectro(file->imatrix[i][j]);
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

INumber* Fourier(int *matriz,int l,int h,int inversa)
{
    int Neg = -1;
    if(inversa)
        Neg = 1;

    int N = h;
    int freq,t;
    INumber *frequencies = malloc(sizeof(INumber) * N);

    for (freq = 0; freq < N; freq++) {
        double re = 0;
        double im = 0;

        for (t = 0; t < N; t++) {
            double time= (double)t;
            double var = matriz[t];

            double rate = Neg * (2.0 * PI) * freq * time / N;

            double re_part = var * cos(rate);
            double im_part = var * sin(rate);

            re += re_part;
            im += im_part;
        }

        if(inversa){
            re = re / N;
            im = im / N;
        }

        frequencies[freq].r = re;
        frequencies[freq].i = im;
    }

    return frequencies;
}

int Espectro(INumber var)
{
    double Esp = log(1 + sqrt(var.r*var.r + var.i*var.i));

    return (int)Esp;
}


