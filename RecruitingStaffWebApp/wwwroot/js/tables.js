class Tables {
    constructor(goBackBtn, mainTable) {
        this.goBackBtn = goBackBtn;
        this.mainTable = mainTable;
    }

    hideAllTables() {
        var tables = document.getElementsByClassName("table");
        for (var i = 0; i < tables.length; i++) {
            if (tables[i].classList.contains("d-none") === false) {
                tables[i].classList.toggle("d-none");
            }
        }
    }

    goBackBtnVisibilityChange() {
        var questionnairesBtn = document.getElementById(this.goBackBtn);
        questionnairesBtn.classList.toggle("d-none");
    }

    goToTable(id) {
        this.hideAllTables();
        this.goBackBtnVisibilityChange();
        var questionCategory = document.getElementById(id);
        questionCategory.classList.toggle("d-none");
    }

    goBack() {
        this.hideAllTables();
        this.goBackBtnVisibilityChange();
        var questionnairesTable = document.getElementById(this.mainTable);
        questionnairesTable.classList.toggle("d-none");
    }
}