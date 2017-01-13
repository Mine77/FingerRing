#include <ShockDataProcess.h>



// Constructors ////////////////////////////////////////////////////////////////
ShockDataProcess::ShockDataProcess(int _windowLen, int _dataAverage, int _thresholdTap, int _thresholdSlide, int _noiseLevel)
{
    windowLen=_windowLen;
    dataAverage=_dataAverage;
    thresholdTap=_thresholdTap;
    thresholdSlide=_thresholdSlide;
    noiseLevel=_noiseLevel;
    
	for (int i = 0; i < windowLen; ++i)
	{
		buffer[windowLen]=0;
	}
    ptr = windowLen - 1;
    isBufferFull = false;
    sum = 0;
    average = 0;
}

// Public Methods //////////////////////////////////////////////////////////////

// Add data to buffer
void ShockDataProcess::addData(int _data)
{
	_data-=dataAverage; 	//minus average
	if (_data<0)			//abs
	{
		_data=-_data;
	}
	if (_data<noiseLevel)	//suspend noise
	{
		_data=0;
	}
    MovePtr(1);
    buffer[ptr] = _data;
    if (!isBufferFull)
    {
        sum += _data;
        average = (int)(sum / ptr);
        if (ptr == windowLen - 1)
        {
            isBufferFull = true;
        }
    }
    else
    {
        sum -= ReturnData(1);
        sum += _data;
        average = (int)(sum / windowLen);
    }
}

//return the result base on the current data in the buffer
int ShockDataProcess::getResult()
{
	if (average>thresholdTap)
	{
		return ShockDataProcess::TAP;
	}
	else if (average>thresholdSlide)
	{
		return ShockDataProcess::SLIDE;
	}
	else
	{
		return ShockDataProcess::NONE;
	}
}

//return the data from relevent position:
//pos   -3 -2 -1  0  1  2  3
//data   *  *  *  *  *  *  *
//index  0  1  2  3  4  5  6
int ShockDataProcess::ReturnData(int _pos)
{
    int tempPtr = ptr;
    if (_pos < 0)
    {
        _pos += windowLen;
    }
    tempPtr = (tempPtr + _pos) % windowLen;
    return buffer[tempPtr];
}

void ShockDataProcess::MovePtr(int _times)
{
    if (_times < 0)
    {
        _times += windowLen;
    }
    ptr = (ptr + _times) % windowLen;
}