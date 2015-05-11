#include <windows.h>
#include <stdio.h>
#include <stdlib.h>

#if defined(__APPLE__)
#include <GLUT/glut.h>
#else
#include <GL/glut.h>
#endif

typedef struct _Point{
    int x;
    int y;
}Point;

typedef struct _Line{
    Point *points;
    int numpoints;
}Line;

int nLines;
Line *oLines;
char *FileName = "\\dino.dat";

GLdouble width = 800, height = 800,escala = 60;

int find(char *tchar, char *tfind){
    int fim = 0;
    int achou = 0;

    while(tchar[fim] !=NULL){
        if(tchar[fim] != *tfind)
            fim++;
        else{
            achou = 1;
            break;
        }
    }
    if(!achou)
        fim = 0;

    return fim;
}

char *substr(char *tchar,int tini,int tlen){
    char *retorno = (char*)calloc(tlen,sizeof(char*));
    int i,k=0;

    for(i=tini;i<(tini+tlen);i++){
        retorno[k] = tchar[i];
        k++;
    }

    return retorno;
}

void display(void)
{
    int i,j;
    glClearColor(1.0, 1.0, 1.0, 0.0);
    glClear(GL_COLOR_BUFFER_BIT);

    glColor3f(0.0, 0.0, 0.0);
    for(i=0;i<nLines;i++){
        glBegin(GL_LINE_STRIP);

        for(j=0;j<oLines[i].numpoints;j++){
            glVertex2i(oLines[i].points[j].x,oLines[i].points[j].y);
        }
        glEnd();
    }

  glFlush();
}

void reshape(int w, int h){
  width = (GLdouble) w;
  height = (GLdouble) h;

  glViewport(0, 0, (GLsizei) width, (GLsizei) height);

  glMatrixMode(GL_PROJECTION);
  glLoadIdentity();
  gluOrtho2D(0.0, width, 0.0, height);
}

void CargaInit(){
    size_t nLen = 10;
    char line[nLen];
    int ini = 0;
    int numline = -1,numponts = 0,fim = 0;

    FILE *file = fopen(FileName,"r");

    if (file == NULL){
        printf("Erro Para Abrir o arquivo!");
        return;
    }

    while( fgets(line,nLen,file)!=NULL )
    {
        if(ini==0){
            ini = 1;
            nLines = atoi(substr(line,0,find(line,"\n")));
            oLines = (Line*)calloc(nLines,sizeof(Line));
        }
        else{
            if(find(line," ")==0){
                numline++;
                numponts = 0;
                oLines[numline].numpoints = atoi(substr(line,0,find(line,"\n")));
                oLines[numline].points = (Point*)calloc(oLines[numline].numpoints,sizeof(Point));
            }
            else{
                oLines[numline].points[numponts].x = atoi(substr(line,0,find(line," ")));
                oLines[numline].points[numponts].y = atoi(substr(line,find(line," "),find(line,"\n")));
                numponts++;
            }
        }
    }
    fclose(file);
}

int main(int argc, char *argv[]){

    glutInit(&argc, argv);

    glutInitDisplayMode(GLUT_SINGLE | GLUT_RGBA);

    glutInitWindowSize((int) width, (int) height);

    glutCreateWindow("EAD1" /* title */ );

    glutReshapeFunc(reshape);

    glutDisplayFunc(display);

    CargaInit();

    glutMainLoop();

    return 0;
}
