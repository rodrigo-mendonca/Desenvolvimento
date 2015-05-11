#include <allegro5/allegro.h>

struct Sprite{
      ALLEGRO_BITMAP *oImg;
      int nIXFrame;
      int nIYFrame;
      int nXFrame;
      int nYFrame;
      int nTFrame;
      int nX;
      int nY;
      int nW;
      int nH;
      ALLEGRO_TIMER *oTimer;
   } Sprite;

struct MovSprite{
      struct Sprite *oSprite;
      int nStep;
      int nTipo;
      int nW;
      int nH;
   } MovSprite;

// Variável representando a janela principal
ALLEGRO_COLOR SetColor(int,int,int,int);
ALLEGRO_DISPLAY *oScreen;

int nWigth;
int nHeigth;
char *cTitle;
bool lPause;


void GameLib_Init();
void ClearScreen();
void Wait(double);
void DrawSprite(int,struct Sprite*);
void MoveSprite(struct MovSprite*);


