#include <Wire.h>
#include <VL6180X.h>


VL6180X sensor, sensor27;

int pin_slide = 12;
int pin_tap = 13;

int ProxEN = 5;
int ProxEN_27 = 6;

int data1=0,data2=0,data3=0;

void setup()
{
  pinMode(pin_slide, INPUT);
  pinMode(pin_tap, INPUT);

  pinMode(ProxEN, OUTPUT);
  pinMode(ProxEN_27, OUTPUT);
  digitalWrite(ProxEN, LOW);
  digitalWrite(ProxEN_27, HIGH);

  Wire.begin();
  
  delay(100);//delay is necessary for running stablily 
  sensor27.setAddress(0x27);

  digitalWrite(ProxEN, HIGH);

  delay(100);
  sensor.init();
  sensor27.init();
  sensor.configureDefault();
  sensor27.configureDefault();
  sensor.stopContinuous();
  sensor27.stopContinuous();
//  sensor.writeReg(VL6180X::SYSRANGE__MAX_CONVERGENCE_TIME, 30);  
//  sensor27.writeReg(VL6180X::SYSRANGE__MAX_CONVERGENCE_TIME, 30);

  delay(300);
  sensor.startRangeContinuous(60);
  sensor27.startRangeContinuous(60);

  Serial.begin( 115200 );
}

void loop()
{
  if (sensor.isRangeContinuousReady())
  {
    data1 = sensor.readRangeContinuous();
    Serial.print(data1);
  }
  else
  {
    Serial.print(data1);
  }
  Serial.print(",");

  if (sensor27.isRangeContinuousReady())
  {
    data2 = sensor27.readRangeContinuous();
    Serial.print(data2);
  }
  else
  {
    Serial.print(data2);
  }
  Serial.print(",");

  Serial.print(0);

  Serial.print('\r');
}









