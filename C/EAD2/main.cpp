#include <windows.h>
#include <stdio.h>
#include <stdlib.h>
#include <math.h>

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
    Point oPoint1;
    Point oPoint2;
}Line;

Line *oLines;
int nLines  = 0;
int DPoints = 0,DView = 0;

GLdouble width      = 400, height      = 400,escala = 60;
GLdouble subwidth   = 400, subheight   = 400;
GLdouble subx       = 0, suby          = 0;
GLdouble lastx      = 0, lasty         = 0;

void EditPoint(int *tx,int *ty){
    if(*tx < subx)
        *tx = subx;

    if(*tx > subwidth)
        *tx = subwidth;

    if(*ty < suby)
        *ty = suby;

    if(*ty > subheight)
        *ty = subheight;

}

void ViewPoint(Line oLines){
    double nAng,nLin1,nLin2;

    int nX1 = oLines.oPoint1.x;
    int nX2 = oLines.oPoint2.x;
    int nY1 = oLines.oPoint1.y;
    int nY2 = oLines.oPoint2.y;

    int nX,nY;
    // calculo de angular
    if ((nX2 - nX1)==0)
        nAng = 0;
    else
        nAng = ((double) (nY2 - nY1) / (nX2 - nX1));

    // calculo linear
    nLin1 = (double)(nY1-(nAng*nX1));
    nLin2 = (double)(nY2-(nAng*nX2));

    nX = nX1;
    nY = nY1;
    EditPoint(&nX,&nY);

    if (nAng!=0)
        nX = (int) round((nY - nLin1)/nAng);

    nY = (int) round(nAng*nX + nLin1);

    glVertex2d(nX,nY);

    nX = nX2;
    nY = nY2;
    EditPoint(&nX,&nY);

    if (nAng!=0)
        nX = (int) round((nY - nLin2)/nAng);

    nY = (int) round(nAng*nX + nLin2);

    glVertex2d(nX,nY);
}

void display(void)
{
    int i;
    int x,y;
    glClearColor(1.0, 1.0, 1.0, 0.0);
    glClear(GL_COLOR_BUFFER_BIT);

    glColor3f(0.0, 0.0, 0.0);
    for(i=0;i<nLines;i++){
        glBegin(GL_LINES);
            ViewPoint(oLines[i]);

        glEnd();
    }

    glBegin(GL_LINE_LOOP);
        glVertex2i(subx,suby);
        glVertex2i(subwidth,suby);
        glVertex2i(subwidth,subheight);
        glVertex2i(subx,subheight);
    glEnd();

    glFlush();
}

void reshape(int w, int h){
  width = (GLdouble) w;
  height = (GLdouble) h;

  glViewport(0, 0, (GLsizei) width, (GLsizei) height);

  glMatrixMode(GL_PROJECTION);
  glLoadIdentity();
  gluOrtho2D(0.0, width, height, 0.0);
}

void _MouseCall(int btn, int state, int tnX, int tnY){

    if(btn==2 && state==1){
        if(DPoints==0){
            oLines[nLines].oPoint1.x = tnX;
            oLines[nLines].oPoint1.y = tnY;
            DPoints = 1;
        }
        else{
            oLines[nLines].oPoint2.x = tnX;
            oLines[nLines].oPoint2.y = tnY;
            DPoints = 0;
            nLines++;
        }
    }

    if(btn==0 && state==1){
         if(DView==0){
            lastx = tnX;
            lasty = tnY;
            DView = 1;
        }
        else{
            if(lastx < tnX){
                subx      = lastx;
                subwidth  = tnX;
            }
            else{
                subx = tnX;
                subwidth  = lastx;
            }

            if(lasty < tnY){
                suby = lasty;
                subheight  = tnY;
            }
            else{
                suby = tnY;;
                subheight  = lasty;
            }
            DView = 0;
        }
    }
    glutPostRedisplay();
}

void _MouseMove(int tnX, int tnY){

}

int main(int argc, char *argv[]){
    oLines = (Line*) calloc(100,sizeof(Line));

    glutInit(&argc, argv);

    glutInitDisplayMode(GLUT_SINGLE | GLUT_RGBA);

    glutInitWindowSize((int) width, (int) height);

    glutCreateWindow("EAD2" /* title */ );

    glutReshapeFunc(reshape);

    glutMouseFunc (_MouseCall);

    glutMotionFunc(_MouseMove);

    glutDisplayFunc(display);

    glutMainLoop();

    return 0;
}
