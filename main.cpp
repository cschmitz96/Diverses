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

QString create_fish_left(QStringList left, int steps, int gos){
    QString swim = "";
    int leng = left.length();
    for( int a = 0; a < leng; a++){
        for(int to_go = gos; to_go > steps; to_go--){
            swim.append(QString("     "));
        }
        if(a == 2){
            swim.append(QString("  o"));
        }
        else{
            swim.append(QString("   "));
        }
        if(a<2){
//            random_Bubble();
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

QString create_fish_right(QStringList right, int steps, int gos){
    QString swim = "";
    int leng = right.length();
    for( int a = 0; a < leng; a++){
        for(int gone = 0; gone < steps; gone++){
            swim.append(QString("     "));
        }
        swim.append(QString(right[a]));
        if(a == 2){
            swim.append(QString("o  "));
        }
        else{
            swim.append(QString("   "));
        }
        if(a<2){
//            random_Bubble();
        }
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

void swim(QStringList fish, int gos, bool right){
    for(int steps =0; steps<gos; steps++){
        QString swim;
        if(right){
            swim = create_fish_right(fish, steps, gos);
        }
        else{
            swim = create_fish_left(fish, steps, gos);
        }
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
    QString fish_left_1 = "      .       ";
    QString fish_left_2 = "_____/(_____/ ";
    QString fish_left_3 = ">'__ ____v--\\ ";
    QString fish_left_4 = "   \\(         ";
    QString fish_left_5 = "    '         ";

    QString fish_right_1= "       .      ";
    QString fish_right_2 =" \\_____)\\_____";
    QString fish_right_3= " /--v____ __'<";
    QString fish_right_4= "         )/   ";
    QString fish_right_5= "         '    ";


    QStringList left = {fish_left_1, fish_left_2, fish_left_3, fish_left_4, fish_left_5};
    QStringList right = {fish_right_1, fish_right_2, fish_right_3, fish_right_4, fish_right_5};

    int swims = rand() %5;

    int r;
    r = rand() %20;
    for(int i =0; i <= swims; i++){
        swim(left, r, false);
        r = rand() %20;
        swim(right, r, true);
        r= r-1;
    }
}
