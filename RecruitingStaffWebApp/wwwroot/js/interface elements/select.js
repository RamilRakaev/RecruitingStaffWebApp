let multiselect_block = document.querySelectorAll(".multiselect_block");
multiselect_block.forEach(parent => {
    let label = parent.querySelector(".field_multiselect");
    let select = parent.querySelector(".field_select");
    let text = label.innerHTML;
    removeSelected(select);
    select.addEventListener("change", function (element) {
        let selectedOptions = this.selectedOptions;
        label.innerHTML = "";
        for (let option of selectedOptions) {
            let button = document.createElement("button");
            button.type = "button";
            button.className = "btn_multiselect";
            button.textContent = option.text;
            button.onclick = _ => {
                option.selected = false;
                button.remove();
                if (!select.selectedOptions.length) label.innerHTML = text
            };
            label.append(button);
        }
    })
})
function removeSelected(select) {
    let selectedOptions = select.selectedOptions;
    for (let option of selectedOptions) {
        option.selected = false;
    }
}