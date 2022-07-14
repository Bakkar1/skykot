//bach yban lik path dyal file f labale
window.addEventListener('load', function () {
    let myInput = document.querySelector('.custom-file-input');
    myInput.onchange = function () {
        //let fileName = this.value.split('\\').pop();
        //this.nextElementSibling.textContent = fileName;
        let selectedItems = this.files.length;
        if (selectedItems == 1) {
            this.nextElementSibling.textContent = this.value.split('\\').pop();
        }
        else if (selectedItems > 1) {
            this.nextElementSibling.textContent = selectedItems + " files selected";
        }
    }
}, false)