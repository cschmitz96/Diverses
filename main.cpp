#include <QLabel>
#include <QMainWindow>
#include <QApplication>
#include <QString>
#include <QByteArray>
#include <QDebug>
#include <QTime>

void delay()
{
    QTime dieTime= QTime::currentTime().addSecs(1);
    while (QTime::currentTime() < dieTime)
        QCoreApplication::processEvents(QEventLoop::AllEvents, 100);

}

QString create_fish_left(QString left[], int steps, int gos){
    QString swim = "";
    int leng = 3;
//    int leng = left.length();
    for( int a = 0; a < leng; a++){
        for(int to_go = gos; to_go > steps; to_go--){
            swim.append(QString("     "));
        }
        swim.append(QString(left[a]));
        for(int gone = 0; gone < steps; gone++){
            swim.append(QString("     "));
        }
        if(a + 1 == leng){
            swim.append(QString(""));
        }
        else{
            swim.append(QString("\n"));
        }
    }
    return swim;
}

QString create_fish_right(QString right[], int steps, int gos){
    QString swim = "";
    int leng = 3;
    for( int a = 0; a < leng; a++){
        for(int gone = 0; gone < steps; gone++){
            swim.append(QString("     "));
        }
        swim.append(QString(right[a]));
        for(int to_go = gos; to_go > steps; to_go--){
            swim.append(QString("     "));
        }
        if(a + 1 == leng){
            swim.append(QString(""));
        }
        else{
            swim.append(QString("\n"));
        }
    }
    return swim;
}

void swim_left(QString left[], int gos){
    for(int steps =0; steps<gos; steps++){
        QString swim = create_fish_left(left, steps, gos);
        QString swims = QString("%1").arg(swim);
        const char *str;
        QByteArray ba;
        ba = swims.toLatin1();
        str = ba.data();
        qDebug() << "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";
        qDebug() << str;
        delay();

     }
}

void swim_right(QString right[], int gos){
    for(int steps =0; steps<gos; steps++){
        QString swim = create_fish_right(right, steps, gos);
        QString swims = QString("%1").arg(swim);
        const char *str;
        QByteArray ba;
        ba = swims.toLatin1();
        str = ba.data();
        qDebug() << "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";
        qDebug() << str;
        delay();

    }
}

int main(int argc, char **argv)
{

    srand (time(NULL));
//    QApplication application(argc, argv);
//    QMainWindow mainWindow;
    QString fish_left_1 = " _____/(_____/ ";
    QString fish_left_2 = " >'__ ____v--\\ ";
    QString fish_left_3 = "    \\(         ";
    QString fish_right_1 =" \\_____)\\_____ ";
    QString fish_right_2= " /--v____ __'< ";
    QString fish_right_3= "         )/    ";
    QString left[3] = {fish_left_1, fish_left_2, fish_left_3};
    QString right[3] = {fish_right_1, fish_right_2, fish_right_3};


    int swims = rand() %5;

    int r;
    r = rand() %20;
    for(int i =0; i <= swims; i++){
        swim_left(left, r);
        r = rand() %20;
        swim_right(right, r);
        r= r-1;
    }
}
