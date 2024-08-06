window.onload = function () {
    new EmployeePage();
}

class EmployeePage {
    employeesBackup = [];
    employees = [];
    sizeOfPage = 20;
    pageNumber = 1;
    totalPage = 0;

    pageTitle = "Quản lý nhân viên";
    constructor() {
        this.initEvents();
    }

    /**
     * Khởi tạo sự kiện trong employee page
     * Author: Ngô Minh Hiếu (16-7-2024)
     */

    initEvents() {
        try {
            // lấy dữ liệu từ server
            this.loadData();

            // Bắt sự kiện cho ô input search
            document.querySelector("#search-input").addEventListener('keydown', (event) => {
                let key = document.querySelector("#search-input").value?.trim().toLowerCase();
                if (event.key == "Enter" && key !== null && key !== "" && key !== undefined) {
                    let tmp = [];
                    console.log(key);
                    for (const item of this.employeesBackup) {
                        if (item.FullName.toLowerCase().includes(key)) {
                            tmp.push(item);
                        }
                    }
                    this.employees = tmp;

                    this.updateTable();
                } if (key === "") {
                    this.employees = this.employeesBackup;
                    this.updateTable();
                }
            });

            //Bắt sự kiện cho reload page
            document.querySelector(".table-search").querySelector("button").nextElementSibling.addEventListener('click', () => {
                this.employees = [];
                this.pageNumber = 1;
                this.totalPage = 0;
                this.genderTable();
                this.loadData();
            });

            // Bắt sự kiện cho arrow left
            document.querySelector("#arrow-left").addEventListener('click', () => {
                if (this.pageNumber > 1) {
                    console.log(this);
                    this.pageNumber -= 1;
                    this.genderTable();
                }
            });

            // Bắt sự kiện cho arrow right
            document.querySelector("#arrow-right").addEventListener('click', () => {
                if (this.pageNumber < this.totalPage) {
                    console.log(this);
                    this.pageNumber += 1;
                    this.genderTable();
                }
            });

            //Bắt sự kiện cho thẻ select
            document.querySelector("#dropdown-page").addEventListener('change', () => {
                this.pageNumber = document.querySelector("#dropdown-page").value;
                this.genderTable();
            });

            // Tắt popup
            document.querySelector("#popup").querySelector("button").addEventListener("click", function () {
                this.parentElement.parentElement.parentElement.style.display = "none";
            });

            // bắt sự kiện cho button thêm mới và update
        } catch (error) {
            console.error(error);
        }
    }

    /**
     * Cập nhật lại bảng mỗi khi tìm kiếm
     * Author: Ngô Minh Hiếu (16-7-2024)
     */
    updateTable() {
        try {
            this.totalPage = Math.floor(this.employees.length / this.sizeOfPage) + 1;
            this.genderSelectOption();
            this.pageNumber = 1;
            document.querySelector(".page-container").querySelector(".total-page").innerHTML = `Tổng: ${this.employees.length}`;
            this.genderTable();
        } catch (error) {
            console.error(error)
        }
    }

    /**
     * Bắt sự kiện cho nút đóng popup
     * Author: Ngô Minh Hiếu (16-7-2024)
     */
    buttonDeleteAddOnClick() {
        try {
            let popup = document.querySelector("#popup");
            popup.querySelector(".popup-header").firstElementChild.innerHTML = "Bạn muốn xóa?"
            popup.querySelector(".popup-body").innerHTML = `<p>Bạn muốn xóa thông điệp này</p>`;
            popup.style.display = "block";
            console.log(popup);

        } catch (error) {
            console.error(error);
        }
    }

    
    /**
     * Bắt sự kiện cho button thêm mới và update
     * Author: Ngô Minh Hiếu (16-7-2024)
     */
    buttonAddOnClick() {
        try {
            document.querySelector(".button-add").forEach(button => {
                console.log(button);
            })
        } catch (error) {
            console.log(error);
        }
    }

    /**
     * Lấy dữ liệu từ server
     * Author: Ngô Minh Hiếu (16-7-2024)
     */
    loadData() {
        try {
            fetch("https://cukcuk.manhnv.net/api/v1/Employees")
                .then(res => res.json())
                .then(data => {
                    document.querySelector(".page-container").querySelector(".total-page").innerHTML = `Tổng: ${data.length}`;
                    this.employees = data;
                    this.employeesBackup = data;
                    this.totalPage = Math.floor(this.employees.length / this.sizeOfPage) + 1;
                    this.genderSelectOption();
                    this.genderTable();
                })
        } catch (error) {
            console.error("Lỗi : " + error);
        }
    }

    /**
    * đổ dữ liệu lên bảng
    * Author: Ngô Minh Hiếu (17-7-2024)
    */
    genderTable() {
        try {
            const table = document.querySelector("#tblEmployee");
            document.querySelector("#dropdown-page").value = this.pageNumber;
            let maxLoop = this.pageNumber === this.totalPage ? this.employees.length : this.sizeOfPage * this.pageNumber;

            table.querySelector("tbody").innerHTML = "";
            for (let i = maxLoop - 20; i < maxLoop; i++) {
                let tr = document.createElement("tr");
                tr.innerHTML = `
                    <td>${i + 1}</td>
                    <td>${this.employees[i].EmployeeCode}</td>
                    <td>${this.employees[i].FullName}</td>
                    <td>${this.employees[i].Gender === 1 ? "Nam" : "Nữ"}</td>
                    <td>${this.employees[i].DateOfBirth?.substr(0, 10)}</td>
                    <td>${this.employees[i].Email}</td>
                    <td style="position: relative;">
                        ${this.employees[i].Address}
                        <div>
                            <button class="edit-btn button-img button-edit"><img class="img" src="../assets/icon/icons8-edit-24.png" alt=""></button>
                            <button class="delete-btn button-img button-add" data-id="${this.employees[i].EmployeeId}"><img class="img" src="../assets/icon/close-48.png" alt=""></button>
                        </div>
                    </td>`;
                table.querySelector("tbody").append(tr);

                this.buttonAddOnClick();
            }

            // Bắt sự kiện cho các button được tạo ra cùng với bảng
            let buttons = document.querySelectorAll(".delete-btn");
            for (const button of buttons) {
                button.addEventListener("click", this.buttonDeleteAddOnClick);
            }
        } catch (error) {
            console.log(error);
        }
    }

    /**
    * Diền số trang và thẻ select
    * Author: Ngô Minh Hiếu (17-7-2024)
    */
    genderSelectOption() {
        try {
            const select = document.querySelector("#dropdown-page");
            for (let i = this.totalPage; i > 0; i--) {
                let option = document.createElement("option");
                option.value = i;
                option.textContent = i;
                select.appendChild(option);
            }
        } catch (error) {
            console.log(error);
        }
    }
}