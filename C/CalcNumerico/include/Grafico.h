#ifndef GRAFICO_H
#define GRAFICO_H


class Grafico
{
    public:
        Grafico();
        virtual ~Grafico();

        void CriarGrafico();

        int nEscala;
        int nAltura = 1000,nLargura = 1000;
        char* cName;
    protected:
    private:
        int nXCentro = nAltura / 2;
        int nYCentro = nLargura / 2;

        void DesenhaEscala (void);
        double PixelToCode (int, char);
        int CodeToPixel (double, char);
        void DesenhaPonto (double,double);
        void DesenhaLinha (double,double,double,double);
};

#endif // GRAFICO_H
