f = open("Resources/words.txt","r")

lines = f.readlines()

f.close()

f = open("Resources/words.txt","w")



for line in lines:
	if len(line) > 3:
		f.write(line)
	else:
		print("Removed: "+line)

f.close()
