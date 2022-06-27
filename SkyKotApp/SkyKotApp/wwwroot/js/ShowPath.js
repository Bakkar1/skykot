window.addEventListener('load', function () {
    let myInput = document.querySelector('.custom-file-input');
    if (document.contains(myInput)) {
        myInput.onchange = function () {
            let fileName = this.value.split('\\').pop();
            this.nextElementSibling.textContent = fileName;
        }
    }
}, false)