#include <Arduino.h>

const int switchPins[8] = {22, 23, 24, 25, 26, 27, 28, 29};
const int ledPins[8] = {30, 31, 32, 33, 34, 35, 36, 37};

int activeLED = -1;
unsigned long startTime;
bool flickerStarted = false;

bool switchPrevStates[8];
unsigned long timeLimit = 5000;
const unsigned long minTimeLimit = 1000;
const unsigned long decrementStep = 500; 

void setup() {
  Serial.begin(9600);

  for (int i = 0; i < 8; i++) {
    pinMode(switchPins[i], INPUT_PULLUP);
    pinMode(ledPins[i], OUTPUT);
    digitalWrite(ledPins[i], LOW); 
    switchPrevStates[i] = HIGH;
  }

  randomSeed(analogRead(0)); 
  activateRandomLED();       
}

void loop() {
  if (activeLED >= 0) {

    int switchState = digitalRead(switchPins[activeLED]);

    if (switchPrevStates[activeLED] == LOW && switchState == HIGH) { 
      Serial.println(-5);
      timeLimit = max(minTimeLimit, timeLimit - decrementStep);
      resetGame();
      activateRandomLED();
      return;
    }

    switchPrevStates[activeLED] = switchState;

    unsigned long elapsedTime = millis() - startTime;

    if (elapsedTime > (timeLimit - 2000) && !flickerStarted) {
      flickerStarted = true;
    }

    if (flickerStarted) {
      digitalWrite(ledPins[activeLED], (millis() / 200) % 2); 
    }

    if (elapsedTime > timeLimit) {
      Serial.println("+5");
      timeLimit = 5000;
      resetGame();
      activateRandomLED();
    }
  }
}

void activateRandomLED() {
  activeLED = random(0, 8); 


  digitalWrite(ledPins[activeLED], HIGH); 
  startTime = millis();                  
  flickerStarted = false;        
}

void resetGame() {
  for (int i = 0; i < 8; i++) {
    digitalWrite(ledPins[i], LOW);
    switchPrevStates[i] = digitalRead(switchPins[i]); 
  }
  activeLED = -1;
}
