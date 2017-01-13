# Library for the shock data processing
Use a buffer to store certain amount of past data. Just like using a window to sample the data. 
Every time when adding a data to buffer, caculate one and only one feature that can be used to classify the 'none', 'slide' and 'tap'.
(abandon all the data in buffer once a slide or tap is found?)
If the result is slide or tap than flip the certain pins to trigger the interrupt on the main arduino.

----------------------------------------------------------
20170111
test with a sample and works pretty well

1.Minus Average
2.ABS
3.Suspend Noise (substract noise level)
4.Window Average
5.Tri Level Convert

Here's the parameters in this test
DATA_AVERAGE = 655
WINDOW_LENGTH = 270
THRESHOLD_SLIDE = 5
THRESHOLD_TAP = 120
NOISE_LEVEL = 25
