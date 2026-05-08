zadacha = [[0, 1, 1, 1, 0, 0, 0],
           [1, 0, 1, 0, 1, 0, 1],
           [1, 1, 0, 1, 1, 1, 1],
           [1, 0, 1, 0, 1, 1, 1],
           [0, 1, 1, 1, 0, 1, 0],
           [0, 0, 1, 1, 1, 0, 1],
           [0, 1, 1, 1, 0, 1, 0]]

def check(way):
    for i in way:
        print(i + 1, end=',')
    print(end=" ")
    if zadacha[way[0]][way[6]] != 0:
        print('цикл')
    else:
        print('путь')



way = [0, 0, 0, 0, 0, 0, 0]



def check(way):
    for i in way:
        print(i + 1, end=',')
    print(end=" ")
    if zadacha[way[0]][way[6]] != 0:
        print('цикл')
        return 1
    else:
        print('путь')
        return 0

way = [0] * 7
cycles = 0
roads = 0

for i1 in range(7):
    way[0] = i1
    for i2 in range(7):
        if zadacha[i1][i2] == 0 or i2 in way[:1]:
            continue
        way[1] = i2
        for i3 in range(7):
            if zadacha[i2][i3] == 0 or i3 in way[:2]:
                continue
            way[2] = i3
            for i4 in range(7):
                if zadacha[i3][i4] == 0 or i4 in way[:3]:
                    continue
                way[3] = i4
                for i5 in range(7):
                    if zadacha[i4][i5] == 0 or i5 in way[:4]:
                        continue
                    way[4] = i5
                    for i6 in range(7):
                        if zadacha[i5][i6] == 0 or i6 in way[:5]:
                            continue
                        way[5] = i6
                        for i7 in range(7):
                            if zadacha[i6][i7] == 0 or i7 in way[:6]:
                                continue
                            way[6] = i7
                            if check(way):
                                cycles += 1
                            else:
                                roads += 1
print('Всего циклов:', cycles)
print('Всего дорог', roads)