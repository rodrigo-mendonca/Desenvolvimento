#include <windows.h>
#include <GL/glut.h>
#include <cstdlib>
#include <stdio.h>
#include <ctime>

#define WIDTH 600
#define HEIGTH 600
// NUMERO DE VEZES QUE DIVIDE CADA TRIANGULO
int nIterations = 5;

// Metodos
double Random(bool reset = false);
void DrawTriangle(GLfloat *, GLfloat *, GLfloat *);
void DivideTriangle(GLfloat *, GLfloat *, GLfloat *, int);
void Keyboard(unsigned char, int, int);
void KeySpecial(int, int, int);
void Display();
void Zoom();
void Init();

//coordenadas do triangulo
/*
coordenadas em float definem a porgetagem da posição da tela,
a tela é dividida, onde o ponto (0,0), é o centro,
tudo para a esquerda é o (-1,0)
tudo para baixo é o (0,-1)
*/
GLfloat nTriangle[3][2] = {
    {-1.0, -1.0},
    { 0.0, 1.0},
    { 1.0, -1.0}
};

double nColors[1000] = {0};
double nZoom = 0;

int main(int argc, char** argv) {
    Init();
    glutDisplayFunc(Display);
    glutMainLoop();
}

void Init() {
    srand(time(NULL));

    // cores para os triangulos
    for (int i = 0; i < 1000; i++) {
        nColors[i] = rand() / (double) RAND_MAX;
    }

    glutInitDisplayMode(GLUT_RGB);
    glutInitWindowSize(WIDTH, HEIGTH);
    glutInitWindowPosition(0, 0);
    glutCreateWindow("Triangulo Sierpinski");
    glutPositionWindow(100, 100);
    glutKeyboardFunc(Keyboard);
    glutSpecialFunc(KeySpecial);

    glClearColor(0.0, 0.0, 0.0, 1.0);
    glColor3f(0.0, 0.0, 0.0);
    glMatrixMode(GL_PROJECTION);
    glLoadIdentity();
    gluOrtho2D(-1.0, 1.0, -1.0, 1.0);
}

double Random(bool lReset) {
    static int nCurr = 0;

    if (lReset) {
        nCurr = 0;
    return 0.0;
    } else {
        if (nCurr >= 1000) nCurr = 0;
        return nColors[nCurr++];
    }
}

void DrawTriangle(GLfloat *nA, GLfloat *nB, GLfloat *nC) {
    glPolygonMode(GL_FRONT_AND_BACK,GL_FILL);
    glShadeModel(GL_SMOOTH);
    glBegin(GL_TRIANGLES);

    glColor3f(Random(), Random(), Random());
    glVertex2fv(nA);
    glColor3f(Random(), Random(), Random());
    glVertex2fv(nB);
    glColor3f(Random(), Random(), Random());
    glVertex2fv(nC);
    glEnd();
}

void DivideTriangle(GLfloat *nA, GLfloat *nB, GLfloat *nC, int tnIteraciones) {
    GLfloat nV[3][2];
    int nJ;
    // numero de vezes que desse um "nivel"
    if (tnIteraciones > 0) {
        //encontra os pontos medios do triangulo
        for (nJ = 0; nJ < 2; nJ++) {
            nV[0][nJ] = (nA[nJ] + nB[nJ]) / 2;
        }
        for (nJ = 0; nJ < 2; nJ++) {
            nV[1][nJ] = (nA[nJ] + nC[nJ]) / 2;
        }
        for (nJ = 0; nJ < 2; nJ++) {
            nV[2][nJ] = (nB[nJ] + nC[nJ]) / 2;
        }

        // para cada triangulo, divide ele em mais 3 triangulos
        DivideTriangle( nA   , nV[0] , nV[1] , tnIteraciones-1);
        DivideTriangle(nV[0], nB     , nV[2] , tnIteraciones-1);
        DivideTriangle(nV[1], nV[2] , nC     , tnIteraciones-1);
        // commenting this will create a Sierpinski Triangle

    } else {
        //dibujar el triángulo de la iteración 0
        DrawTriangle(nA,nB,nC);
    }
}

void Keyboard(unsigned char cKey, int nX, int nY) {


    switch (cKey) {
        case '+':
            if (nIterations < 10) nIterations += 1;
                Display();
            break;
        case '-':
            if (nIterations > 0) nIterations -= 1;
                Display();
            break;
        case 'q':
            exit(0);
            break;
    }
}

void KeySpecial(int nKey, int nX, int nY) {
    switch (nKey) {
        case GLUT_KEY_UP:
            if (nZoom - 0.1 > -1) nZoom -= 0.05;
                Zoom();
            break;
        case GLUT_KEY_DOWN:
            nZoom += 0.05;
                Zoom();
            break;
    }
}

void Zoom() {
    glMatrixMode(GL_PROJECTION);
    glLoadIdentity();
    gluOrtho2D(-1.0 - nZoom, 1.0 + nZoom, -1.0 - nZoom, 1.0 + nZoom);
    glMatrixMode(GL_MODELVIEW);
    Display();
}

void Display() {
    glClear(GL_COLOR_BUFFER_BIT);
    Random(true);
    //al llamar la función dividirTriangulo se indica como cuarto parámetro el número de iteraciones de subdivisión que se quieren
    DivideTriangle(nTriangle[0], nTriangle[1], nTriangle[2], nIterations);
    glFlush();
}


