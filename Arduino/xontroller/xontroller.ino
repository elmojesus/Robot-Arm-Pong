#include <Servo.h>

Servo Vertical;//250 diff  1250-1750
Servo Horizontal;//300 diff 1250 - 1850

bool reading = false;

byte input[2];
int curr = 0;

float vert_value;
float hor_value;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(38400);

  Vertical.attach(8);
  Horizontal.attach(9);
}

void loop() {
  // put your main code here, to run repeatedly:
  
  if (Serial.available() > 0){
    byte b = Serial.read();  
    
    if(reading){
      input[curr] = b;
      curr ++;
    }
    
    if (b == 254){
      reading = true; 
      curr = 0; 
    }
    if (b == 255){
       reading = false;
       calculate();
       move_servo();
    }
  }
}

void calculate(){
  vert_value = (float)input[1] / 253;
  if((int)input[0] == 0){
    vert_value *= -1;
  }
}

void move_servo(){
  Vertical.writeMicroseconds(1500 + (vert_value * 250));
}
