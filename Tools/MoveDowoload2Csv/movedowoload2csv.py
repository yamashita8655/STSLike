# coding: utf-8
#設計
#

import os
import sys
import shutil

def MoveDownload2Csv(downloadPath, csvPath):
    rootList = os.listdir(downloadPath)
    findString = 'STSマスター - '
    #downloadDir = [f for f in rootList if os.path.isdir(os.path.join(path, f))]
    downloadDir = [f for f in rootList if f.startswith(findString) == True]
    for i in range(len(downloadDir)):
        print(downloadDir[i])
        fileName = downloadDir[i]
        fileName = fileName.replace(findString, "")
        currentPath = downloadPath + "\\" + downloadDir[i]
        movePath = csvPath + "\\" + fileName
        shutil.move(currentPath, movePath)
        

# まずは、ディレクトリ走査
downloadPath = 'C:\\Users\\miyashita\\Downloads'
csvPath = 'D:\\github\\STSLike\Assets\\Resources\\Csv'
MoveDownload2Csv(downloadPath, csvPath)
