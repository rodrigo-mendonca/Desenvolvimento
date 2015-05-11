#include <stdio.h>
#include <stdlib.h>
#include <string.h>

void CarregaDados();

char *datafile = "wine.data";
double **tabdados;

typedef struct _dados {
    int Alcohol;
    int Maliccid;
    int Ash;
    int Alcalinityofash;
    int Magnesium;
    int Totalphenols;
    int Flavanoids;
    int Nonflavanoidphenols;
    int Proanthocyanins;
    int Colorintensity;
    int Hue;
    int dilutedwines;
    int Proline;
} dados;

int qtvinhos = 178;
dados *vinhos;


int main()
{
    int i;
    //vinhos = (dados*)calloc(qtvinhos,sizeof(dados));

    tabdados = (double**)calloc(qtvinhos,sizeof(double*));

    for(i=0;i<qtvinhos;i++)
        tabdados[i] = (double*)calloc(13,sizeof(double));

    CarregaDados();

    return 0;
}

void CarregaDados()
{
    FILE *fFile =  fopen(datafile,"r");

    if(fFile == NULL)
        return;

    char *line = NULL,*Var = NULL,*cValue = NULL;
    size_t nLen = 100;
    int i,j = 0,nline = 0,ncolumn = 0;

    //alocando spaçosss
    line    =(char*) calloc(nLen,sizeof(char));
    Var     =(char*) calloc(10,sizeof(char));

    //limpando espaço alocado
    memset(line,'\0',nLen);
    memset(Var,'\0',10);

    while( fgets(line,nLen,fFile) )
	{
        for(i=0; i<nLen; i++)
        {
            if(line[i] != ','){
                Var[j] = line[i];
                j++;
            }

            if(line[i] == ',' || line[i] == '\n'){
                tabdados[nline][ncolumn] = atof(Var);
                j=0;
                memset(Var,'\0',10);
                ncolumn++;
            }
            if(line[i] == '\n')
                break;

        }
        ncolumn=0;
        nline++;
    }

    free(line);
    free(Var);
    fclose(fFile);
}
