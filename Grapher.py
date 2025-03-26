#create a plot based on two text files
import matplotlib.pyplot as plt
import numpy as np

#read the data from the files
data1 = np.loadtxt('time.txt')
data2 = np.loadtxt('output.txt')

#plot the data
plt.plot(data1, data2)
plt.xlabel('time')
plt.ylabel('Amplitude')
plt.title('time vs Amplitude')
plt.show()


