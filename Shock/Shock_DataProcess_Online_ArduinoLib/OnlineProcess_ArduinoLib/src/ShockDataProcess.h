#ifndef SHOCKDATAPROCESS_h
#define SHOCKDATAPROCESS_h

#include <Arduino.h>
#define MaxWindowLength 300


class ShockDataProcess
{
  public:
    enum Result
    {
      NONE=0,
      SLIDE=100,
      TAP=200,
    };


    ShockDataProcess(int _windowLen, int _dataAverage, int _thresholdTap, int _thresholdSlide, int _noiseLevel);

    void addData(int _data);
    int getResult();

  private:
    int windowLen;
    int dataAverage;
    int thresholdTap;
    int thresholdSlide;
    int noiseLevel;

    int buffer[MaxWindowLength];
    bool isBufferFull;
    uint32_t sum;
    int average;
    int ptr;

    int ReturnData(int pos);
    void MovePtr(int times);
};

#endif



