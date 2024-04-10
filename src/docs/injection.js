
// if(document.getElementsByTagName('body').className == 'homepage'){
//     navbar = document.getElementById('navbar-collapse')
//     href = navbar.children[0].children[0].children[0].href
    
//     a = document.getElementsByClassName('navbar-brand')
//     a[0].setAttribute('href', href)
// }

function getAllElementFor(name) {
    elements = document.getElementsByTagName(name)
    return elements
}

const items = [
    'h1',
    'h2',
    'h3',
    'h4',
    'h5',
    'h6',
]


collapse = document.getElementById('toc-collapse')
newUl = document.createElement('ul')
newUl.className = "nav flex-column"
collapse.appendChild(newUl)


for (const h of items) {
    elements = getAllElementFor(h)

    for (const el of elements) {
        // console.log(el)
        newli = collapse.children[0].children[0].cloneNode(true)
        newli.children[0].href= '#' + el.id
        newli.children[0].innerText = el.innerHTML
        if (h == 'h2') {
            newli.children[0].setAttribute('style', 'margin-left:1.2em')
        }else if(h == 'h3'){
            newli.children[0].setAttribute('style', 'margin-left:2.4em')
        }else if(h == 'h4'){
            newli.children[0].setAttribute('style', 'margin-left:3.6em')
        }else if(h == 'h5'){
            newli.children[0].setAttribute('style', 'margin-left:4.8em')
        }else if(h == 'h6'){
            newli.children[0].setAttribute('style', 'margin-left:6em')
        }
        newUl.appendChild(newli)
    }
}

collapse.removeChild(collapse.children[0])