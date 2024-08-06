window.onload = function () {
    new EmployeePage();
}

class EmployeePage {
    employeesBackup = [];
    employees = [];
    sizeOfPage = 20;
    pageNumber = 1;
    totalPage = 0;
    currentDelete = 0;
    currentUpdate = "0";

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
                // Kiểm tra nếu phím Enter được nhấn
                if (event.key === "Enter") {
                    let key = document.querySelector("#search-input").value?.trim().toLowerCase();

                    // Nếu key không rỗng hoặc undefined, thực hiện tìm kiếm
                    if (key !== null && key !== undefined) {
                        let tmp = [];
                        console.log(key);
                        key = key.trim().toLowerCase();

                        // Lọc danh sách nhân viên dựa trên tên đầy đủ
                        for (const item of this.employeesBackup) {
                            if (item.fullName.toLowerCase().includes(key)) {
                                tmp.push(item);
                            }
                            // console.log(`${item.fullName.toLowerCase()} / ${key} / ${item.fullName.toLowerCase().includes(key)}`);
                        }
                        this.employees = tmp;
                        console.log(tmp);
                        // Cập nhật bảng với danh sách nhân viên đã lọc
                        this.updateTable();
                    } else {
                        // Nếu key rỗng, khôi phục danh sách nhân viên gốc
                        this.employees = this.employeesBackup;
                        this.updateTable();
                    }
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

            // bắt sự kiện cho button thêm mới 
            document.querySelector(".button-add").addEventListener("click", () => {
                const newPageUrl = `http://127.0.0.1:5500/Front-end/layout/newemployee.html?id=${this.currentUpdate}`;
                window.location.href = newPageUrl;
            });

            // bắt sự kiện xác nhận xóa của popup
            document.querySelector("#detete-employee-popup-btn").addEventListener("click", () => {
                const requestOptions = {
                    method: "DELETE",
                    redirect: "follow"
                };

                console.log(this.currentDelete);
                fetch(`https://localhost:7178/api/v1/Employees/${this.currentDelete}`, requestOptions)
                    .then((response) => response.json())
                    .then((result) => {
                        console.log(result);
                        this.loadData();
                        // Đóng popup hoặc thực hiện các hành động khác sau khi xóa thành công
                        document.querySelector("#popup").style.display = "none";
                    })
                    .catch((error) => console.error(error));
            });
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
    buttonEditAddOnClick(){
        let employeeId = this.getAttribute("data-id");
        debugger;
        const newPageUrl = `http://127.0.0.1:5500/Front-end/layout/newemployee.html?id=${employeeId}`;
        window.location.href = newPageUrl;
    }


    /**
     * Bắt sự kiện cho nút đóng popup
     * Author: Ngô Minh Hiếu (16-7-2024)
     */
    buttonDeleteAddOnClick(event) {
        try {
            let popup = document.querySelector("#popup");
            popup.querySelector(".popup-header").firstElementChild.innerHTML = "Bạn muốn xóa?";
            popup.querySelector(".popup-body").innerHTML = `<p>Bạn muốn xóa nhân viên này</p>`;
            popup.style.display = "block";
            this.currentDelete = event.currentTarget.getAttribute("data-id");
        } catch (error) {
            console.error(error);
        }
    }

    /**
     * Lấy dữ liệu từ server
     * Author: Ngô Minh Hiếu (16-7-2024)
     */
    loadData() {
        try {
            fetch("https://localhost:7178/api/v1/Employees")
                .then(res => res.json())
                .then(data => {
                    document.querySelector(".page-container").querySelector(".total-page").innerHTML = `Tổng: ${data.length}`;
                    this.employees = data;
                    this.employeesBackup = data;
                    this.totalPage = Math.floor(this.employees.length / this.sizeOfPage) + 1;
                    this.genderSelectOption();
                    this.genderTable();
                    console.log(this.employees)
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

            for (let i = (maxLoop - 20 < 0 ? 0 : maxLoop - 20); i < maxLoop; i++) {
                let tr = document.createElement("tr");
                tr.innerHTML = `
                    <td>${i + 1}</td>
                    <td>${this.employees[i].employeeCode ?? ""}</td>
                    <td>${this.employees[i].fullName ?? ""}</td>
                    <td>${this.employees[i].sex === 1 ? "Nam" : "Nữ"}</td>
                    <td>${this.employees[i].dateOfBirth?.substr(0, 10) ?? ""}</td>
                    <td>${this.employees[i].email ??""}</td>
                    <td style="position: relative;">
                        ${this.employees[i].address?.substr(0, this.employees[i].address.indexOf(", Thành phố")) ?? ""}
                        <div>
                            <button class="edit-btn button-img"   data-id="${this.employees[i].employeeId}"><img class="img" src="../assets/icon/icons8-edit-24.png" alt=""></button>
                            <button class="delete-btn button-img" data-id="${this.employees[i].employeeId}"><img class="img" src="../assets/icon/close-48.png" alt=""></button>
                        </div>
                    </td>`;
                table.querySelector("tbody").append(tr);
            }

            // Bắt sự kiện cho các button update được tạo ra cùng với bảng
            let buttonsEdit = document.querySelectorAll(".edit-btn");
            for (const button of buttonsEdit) {
                button.addEventListener("click",this.buttonEditAddOnClick )
            }

            // Bắt sự kiện cho các button delete được tạo ra cùng với bảng
            let buttonsDelete = document.querySelectorAll(".delete-btn");
            for (const button of buttonsDelete) {
                button.addEventListener("click", (event) => this.buttonDeleteAddOnClick(event));
            }
        } catch (error) {
            console.error(error);
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