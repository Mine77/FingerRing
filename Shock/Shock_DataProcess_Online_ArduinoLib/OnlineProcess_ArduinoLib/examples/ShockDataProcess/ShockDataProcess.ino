/* This minimal example shows how to use the lib */

#include <ShockDataProcess.h>

#define WINDOW_LENGTH 200
#define DATA_AVERAGE 655
#define THRESHOLD_TAP 140
#define THRESHOLD_SLIDE 5
#define NOISE_LEVEL 25

int pin_slide = 7;
int pin_tap = 8;

ShockDataProcess data(WINDOW_LENGTH,DATA_AVERAGE,THRESHOLD_TAP,THRESHOLD_SLIDE,NOISE_LEVEL);

void setup() 
{
  Serial.begin(115200);
  pinMode(pin_slide,OUTPUT);
  pinMode(pin_tap,OUTPUT);

  digitalWrite(pin_slide,LOW);
  digitalWrite(pin_tap,LOW);
}

void loop() 
{ 
  int sensorValue = analogRead(A0);
  
  data.addData(sensorValue);
  int result = data.getResult();
  
   switch(result)
   {
    case ShockDataProcess::NONE:
      digitalWrite(pin_slide,LOW);
      digitalWrite(pin_tap,LOW);
      break;
    case ShockDataProcess::SLIDE:
      digitalWrite(pin_slide,HIGH);
      break;
    case ShockDataProcess::TAP:
      digitalWrite(pin_tap,HIGH);
      break;
   }

//used for sending data to PC
Serial.print("0,");
 Serial.print(sensorValue);
 Serial.print(",");
 Serial.print(result);
 Serial.print('\r');
}

