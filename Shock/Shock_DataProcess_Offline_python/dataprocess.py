import numpy as np
import matplotlib.pyplot as plt

# parameters
DATA_AVERAGE = 655
WINDOW_LENGTH = 270
THRESHOLD_SLIDE = 5
THRESHOLD_TAP = 120
NOISE_LEVEL = 25

# median filter for np_array x with the window length of k
def medianfilt(x, k):
    assert k % 2 == 1, "Median filter length must be odd."
    assert x.ndim == 1, "Input must be one-dimensional."
    k2 = (k - 1) // 2
    y = np.zeros((len(x), k), dtype=x.dtype)
    y[:, k2] = x
    for i in range(k2):
        j = k2 - i
        y[j:, i] = x[:-j]
        y[:j, i] = x[0]
        y[:-j, -(i + 1)] = x[j:]
        y[-j:, -(i + 1)] = x[-1]
    return np.median(y, axis=1)

# max filter for np_array x with the window length of k
def maxfilt(x, k):
    assert x.ndim == 1, "Input must be one-dimensional."
    k2 = (k - 1) // 2
    y = np.zeros((len(x), k), dtype=x.dtype)
    y[:, k2] = x
    for i in range(k2):
        j = k2 - i
        y[j:, i] = x[:-j]
        y[:j, i] = x[0]
        y[:-j, -(i + 1)] = x[j:]
        y[-j:, -(i + 1)] = x[-1]
    return np.amax(y, axis=1)

# power filter for np_array x with the window length of k
def pwrfilt(x, k):
    assert x.ndim == 1, "Input must be one-dimensional."
    y = np.zeros(len(x))
    sum = 0
    for i in range(k):
        sum = sum + x[i]
        y[i] = sum / (i + 1)
    for i in range(k, len(x)):
        y[i] = sum / k
        sum = sum - x[i - k]
        sum = sum + x[i]
    return y

# convert into three level value 0 100 200 for display
def TriLevelConvert(x):
    y = np.zeros(len(x))
    for i in range(len(x)):
        if x[i] > THRESHOLD_TAP:
            y[i] = 700
        elif x[i] > THRESHOLD_SLIDE:
            y[i] = 350
    return y

def suspendNoise(x):
    y = np.zeros(len(x))
    for i in range(len(x)):
        if x[i]>NOISE_LEVEL:
            y[i]=x[i]
    return y

def detectTap():
    pass

# open the data file
with open('data.txt') as f:
    data = []
    for line in f:
        d = line.split()
        data.append(int(d[0]))

# generate the needed array
t = np.arange(len(data))
data_minusAvg = np.subtract(data, DATA_AVERAGE)
data_abs = np.absolute(data_minusAvg)
data_abs = suspendNoise(data_abs) # suspend noise
thresholdArray_slide = np.empty(len(data))
thresholdArray_slide.fill(THRESHOLD_SLIDE)
thresholdArray_tap = np.empty(len(data))
thresholdArray_tap.fill(THRESHOLD_TAP)

result_medianFilt = medianfilt(np.array(data), WINDOW_LENGTH)
result_maxfilt = maxfilt(np.array(data), WINDOW_LENGTH)
result_pwrfilt = pwrfilt(np.array(data_abs), WINDOW_LENGTH)
# result_pwrfilt = result_pwrfilt.astype(int)  # convert to int
result_TriConvert = TriLevelConvert(result_pwrfilt)

# plot the data
# plt.plot(t,data)
plt.plot(t, data_abs)
# plt.plot(t,result_medianFilt)
# plt.plot(t,result_maxfilt)
# plt.plot(t, result_pwrfilt)
plt.plot(t, result_TriConvert)

# plot the threshold line
# plt.plot(t, thresholdArray_slide)
# plt.plot(t, thresholdArray_tap)

# show the plot
plt.show()

#output to file
np.savetxt('data.abs', data_abs, fmt='%d', delimiter='\n')