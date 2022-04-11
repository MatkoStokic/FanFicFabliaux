$(document).ready(function () {
    const file = document.getElementById("Input_Datoteka");
    const text = document.getElementById("Input_Tekst");

    file.addEventListener('change', (event) => {
        disableIfHasValue(event.target.value, text);
    })

    text.addEventListener('change', (event) => {
        disableIfHasValue(event.target.value, file);
    })

    disableIfHasValue(text.value, file);
    disableIfHasValue(file.value, text);

    function disableIfHasValue(value, el) {
        if (value === "") {
            el.removeAttribute('disabled');
        } else {
            el.setAttribute('disabled', 'disabled');
        }
    }});
