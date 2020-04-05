import requests
from bs4 import BeautifulSoup
import re
import os

def GetLinks(url): 
    html = requests.get(url).text
    links = re.findall('(<a class="js-navigation-open".*?</a>)', html, re.S)

    results=[]
    keys=[]
    values=[]
    for link in links:
        soup = BeautifulSoup(link, 'html.parser')
        results.append('[' + soup.a.string + '](https://github.com' + soup.find('a')['href'] + ')') 
        keys.append(soup.a.string)
        values.append('https://github.com' + soup.find('a')['href'])

    return results, keys, values

def WriteMD():
    links, keys, values = GetLinks('https://github.com/zhangsanlzh/Solutions')

    with open('README.md', 'w') as f:
        f.write('# Solutions' + '\n'*2)
        f.write('不断增长的IT解决方案合集。包括.NET，Python，Web，操作系统相关的，开发中遇到的问题的解决方案。' + '\n'*2)        
        f.write('|   Modules   |             Description              |' + '\n')
        f.write('| :---------: | :----------------------------------: |' + '\n')
        f.write('|     DB      |              数据库相关              |' + '\n')
        f.write('|     ETC     |            无明确分类的项            |' + '\n')
        f.write('|     Electron     |            Electron相关的项            |' + '\n')
        f.write('|    Icons    |               各种图标               |' + '\n')
        f.write('|  Micro.NET  |             微软.NET相关             |' + '\n')
        f.write('|     OS      | 操作系统相关(Windows，Linux，Mac OS) |' + '\n')
        f.write('|     Python     |               Python相关               |' + '\n')
        f.write('| ScreenShoot |                 截图                 |' + '\n')
        f.write('|     Web     |               前端相关               |' + '\n')    
        f.write('\n'*2)

        for item in range(9):
            iLinks, iKeys, iValues = GetLinks(values[item])

            f.write('## ' + keys[item] + '\n'*2)
            
            print('正在输出' + keys[item] + '及其子项...')
            for i in range(len(iLinks)):
                f.write(iLinks[i] + '\n'*2)
                # print(iLinks[i] + '\n'*2)
            f.write('\n')

if __name__ == '__main__':
    WriteMD()