#include <Wire.h>
#include <VL6180X.h>


VL6180X sensor;

void setup() {

	Wire.begin();
	Serial.begin( 115200 );
	pinMode(5, OUTPUT);
	pinMode(6, OUTPUT);
	digitalWrite(5, LOW);
	digitalWrite(6, LOW);
	delay(100);
	digitalWrite(5, HIGH);
	if(sensor.readReg(VL6180X::IDENTIFICATION__MODEL_ID) == 0xB4)
		Serial.println("Sensor1 OK!");
	else 
		Serial.println("Sensor1 failure!");
	digitalWrite(5, LOW);
	digitalWrite(6, HIGH);
	delay(100);
	if(sensor.readReg(VL6180X::IDENTIFICATION__MODEL_ID) == 0xB4)
		Serial.println("Sensor2 OK!");
	else 
		Serial.println("Sensor2 failure!");

}

void loop() {
  // put your main code here, to run repeatedly:

}
