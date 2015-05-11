#include <windows.h>
#include <stdio.h>
#include <stdlib.h>

#if defined(__APPLE__)
#include <GLUT/glut.h>
#else
#include <GL/glut.h>
#endif

double nscalex = 1,nscaley = 1;
double nrotx = 0,nroty = 0,nangle = 0;
GLdouble width = 800, height = 800;   /* window width and height */

GLdouble PontX(GLdouble x){
    return x;
}

GLdouble PontY(GLdouble y){
    return y;
}

void initCT(){
    glMatrixMode(GL_MODELVIEW);
    glLoadIdentity();
}

void Scale2D(GLdouble sx,GLdouble sy){
    glMatrixMode(GL_MODELVIEW);
    glScaled(sx,sy,1);
}

void Rotated2D(GLdouble angle,GLdouble sx,GLdouble sy){
    glMatrixMode(GL_MODELVIEW);
    glRotated(angle,sx,sy,1);
}

void display(void)
{
    int i;
    Scale2D(nscalex,nscaley);
    Rotated2D(nangle,nrotx,nroty);

    glClearColor(1.0, 1.0, 1.0, 0.0);
    glClear(GL_COLOR_BUFFER_BIT);

    glColor3f(0.0, 0.0, 0.0);
    glBegin(GL_LINES);
        glVertex2d(PontX(0),PontY(10));
        glVertex2d(PontX(0),PontY(-10));
        glVertex2d(PontX(-10),PontY(0));
        glVertex2d(PontX(10),PontY(0));
    glEnd();

    // casa
    glBegin(GL_LINE_LOOP);
        glVertex2d(PontX(-2),PontY(-2));
        glVertex2d(PontX(-2),PontY(1));
        glVertex2d(PontX(0),PontY(2.5));
        glVertex2d(PontX(2),PontY(1));
        glVertex2d(PontX(2),PontY(-2));

    glEnd();

    //porta
    glBegin(GL_LINE_LOOP);
        glVertex2d(PontX(-1.5),PontY(-2));
        glVertex2d(PontX(-1.5),PontY(-0.5));
        glVertex2d(PontX(-0.8),PontY(-0.5));
        glVertex2d(PontX(-0.8),PontY(-2));
    glEnd();

     //janela
    glBegin(GL_LINE_LOOP);
        glVertex2d(PontX(0.2),PontY(-0.2));
        glVertex2d(PontX(1.7),PontY(-0.2));
        glVertex2d(PontX(1.7),PontY(-1.7));
        glVertex2d(PontX(0.2),PontY(-1.7));
    glEnd();
    glBegin(GL_LINES);
        glVertex2d(PontX(1.9/2),PontY(-0.2));
        glVertex2d(PontX(1.9/2),PontY(-1.7));

        glVertex2d(PontX(0.2),PontY(-1.9/2));
        glVertex2d(PontX(1.7),PontY(-1.9/2));
    glEnd();
    // chamine
    glBegin(GL_LINE_LOOP);
        glVertex2d(PontX(-1.7),PontY(1.23));
        glVertex2d(PontX(-1.7),PontY(2.23));
        glVertex2d(PontX(-1.2),PontY(2.23));
        glVertex2d(PontX(-1.2),PontY(2.23));
    glEnd();

  glFlush();
}

void MoveTo(GLint x,GLint y){

}

void reshape(int w, int h){
  width = (GLdouble) w;
  height = (GLdouble) h;

  //glViewport(0, 0, (GLsizei) width, (GLsizei) height);
    initCT();
  //glMatrixMode(GL_PROJECTION);
  //glLoadIdentity();
  gluOrtho2D(-10, 10, -10, 10);
}

static void Key(unsigned char tcKey, int tnX, int tnY){
    switch (tcKey){
            case 'w':
                nscalex = nscalex*2;
                nscaley = nscaley*2;
                break;
            case 'x':
                nscalex = nscalex*2;
                break;
            case 'y':
                nscaley = nscaley*2;
                break;
            case 'h':
                nangle += 1;
                nrotx = 0;
                nroty = 0;
                break;
            case 'a':
                nangle -= 1;
                nrotx = 0;
                nroty = 0;
                break;
        }
    glutPostRedisplay();
}

int main(int argc, char *argv[]){
  glutInit(&argc, argv);

  glutInitDisplayMode(GLUT_SINGLE | GLUT_RGBA);

  glutInitWindowSize((int) width, (int) height);

  glutCreateWindow("Experiment with line drawing" /* title */ );

  glutReshapeFunc(reshape);
  glutKeyboardFunc(Key);
  glutDisplayFunc(display);

  glutMainLoop();

  return 0;
}
