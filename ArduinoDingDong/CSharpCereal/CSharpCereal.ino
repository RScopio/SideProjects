#include <string.h>

//if you receive dong, send ding
long timer = 0;
long last = 0;
long rate = 50;
bool reading = false;
String command = "";
bool sendDing = false;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);
}

void loop() {
  //send data (delayed)
  long total = millis();
  long delta = total - last;
  last = total;
  timer += delta;
  if(sendDing && timer >= rate)
  {
    sendDing = false;
    timer = 0;
    Serial.print("{ding}");
    command = "";
  }
  
  //read data (fast as possible / use the loop)  
  if(Serial.available() > 0)
  {
    if(!reading && Serial.read() == '{')
    {
      reading = true;
      command = "";
    }
    else if(reading)
    {
      char bite = Serial.read();
      if(bite == '}')
      {
        if(command == "dong")
        {
          sendDing = true;
          command = "";
        }
        reading = false;
      }
      else
      {
        command += bite;
      }
    }
  }
}
