window.onload = function () {
    new NewEmployee();
}

class NewEmployee {
    currentId = "";
    positions = [];
    departments = [];
    data = {};


    constructor() {
        this.currentId = this.getCurrentId();
        this.getDepartment();
        this.getPosition();
        console.log(typeof this.currentId);
        console.log(this.currentId);
        this.initEvents();
    }

    /**
     * Khởi tạo sự kiện trong employee page
     * Author: Ngô Minh Hiếu (16-7-2024)
     */
    initEvents() {
        // lấy dữ liệu của nhân viên trước khi update
        if (this.currentId !== "" && this.currentId !== "0") {
            this.loadEmployeeById();
        } else {
            this.getNewEmployeeCode();
        }

        // hủy sự kiện submit của form
        document.getElementById('register-form').addEventListener('submit', function (event) {
            event.preventDefault();
        });

        // bắt sự kiện cho nút cất
        document.getElementById("button-add").addEventListener('click', this.buttonAddOnClick.bind(this));
    }

    /**
     * Lấy thông tin nhân viên trước khi update
     * Author: Ngô Minh Hiếu (16-7-2024)
     */
    loadEmployeeById() {
        const requestOptions = {
            method: "GET",
            redirect: "follow"
        };


        fetch(`https://localhost:7178/api/v1/Employees/${this.currentId}`, requestOptions)
            .then((response) => response.json())
            .then((data) => {
                document.getElementById('employee-code').value = data.employeeCode;
                document.getElementById('fullname').value = data.fullName;
                document.getElementById('date-of-birth').value = data.dateOfBirth?.split('T')[0] ?? null; // Chỉ lấy phần ngày
                document.querySelector(`input[name="sex"][value="${data.sex === 1 ? 'male' : data.sex === 2 ? 'female' : 'other'}"]`).checked = true;
                document.getElementById('identification-number').value = data.identificationNumber;
                document.getElementById('issue-date').value = data.issueDate?.split('T')[0] ?? null; // Chỉ lấy phần ngày
                document.getElementById('place-of-issue').value = data.placeOfIssue;
                document.getElementById('address').value = data.address;
                document.getElementById('mobile-phone').value = data.mobilePhone;
                document.getElementById('landline-phone').value = data.landlinePhone;
                document.getElementById('email').value = data.email;
                document.getElementById('bank-account').value = data.bankAccount;
                document.getElementById('bank-name').value = data.bankName;
                document.getElementById('brach').value = data.brach;


                // Chọn giá trị trong các select
                document.getElementById('department').value = data.departmentId;
                document.getElementById('position').value = data.positionId;
            })
            .catch((error) => console.error(error));

    }

    /**
     * lấy mã nhân viên mới
     * Author: Ngô Minh Hiếu (16-7-2024)
     */
    getNewEmployeeCode() {
        const requestOptions = {
            method: "GET",
            redirect: "follow"
        };

        fetch("https://localhost:7178/api/v1/Employees/NewEmployee/EmployeeCode", requestOptions)
            .then((response) => response.text())
            .then((result) => {
                document.getElementById('employee-code').value = result;
            })
            .catch((error) => console.error(error));
    }


    /**
     * lấy danh sách các depart ment từ cơ sở dữ liệu và hiển thị lên select
     * Author: Ngô Minh Hiếu (5-8-2024)
     */
    getDepartment() {
        const requestOptions = {
            method: "GET",
            redirect: "follow"
        };

        fetch("https://localhost:7178/api/v1/Departments", requestOptions)
            .then((response) => response.json())
            .then((result) => {

                this.departments = result;

                const select = document.querySelector("#department");
                select.innerHTML = "";
                let options = ``;
                for (const department of this.departments) {
                    options += `<option value="${department.departmentId}">${department.departmentName}</option>`
                }
                select.innerHTML = options;
            })
            .catch((error) => console.error(error));
    }


    /**
     * lấy danh sách các position từ cơ sở dữ liệu và hiển thị lên select
     * Author: Ngô Minh Hiếu (5-8-2024)
     */
    getPosition() {
        const requestOptions = {
            method: "GET",
            redirect: "follow"
        };

        fetch("https://localhost:7178/api/v1/Positions", requestOptions)
            .then((response) => response.json())
            .then((result) => {

                this.positions = result;

                const select = document.querySelector("#position");
                select.innerHTML = "";
                let options = ``;
                for (const position of this.positions) {
                    options += `<option value="${position.positionId}">${position.positionName}</option>`
                }
                select.innerHTML = options;
            })
            .catch((error) => console.error(error));
    }

    /**
     * lấy id từ url id = 0 là thêm mới nhân viên id != 0 là cập nhật
     * Author: Ngô Minh Hiếu (5-8-2024)
     */
    getCurrentId() {
        let url = new URL(window.location.href);
        let params = new URLSearchParams(url.search);
        // Lấy giá trị của tham số 'id'
        return params.get('id');
    }

    /**
     * Bắt sự kiện cho button cất
     * Author: Ngô Minh Hiếu (5-8-2024)
     */
    buttonAddOnClick() {
        // Lấy đối tượng form
        const form = document.getElementById('register-form');

        if (form.checkValidity()) {
            this.getDataEmployee(form);
            debugger;
            if (this.currentId === "0") {
                const myHeaders = new Headers();
                myHeaders.append("Content-Type", "application/json");

                const raw = JSON.stringify(this.data);

                const requestOptions = {
                    method: "POST",
                    headers: myHeaders,
                    body: raw,
                    redirect: "follow"
                };

                fetch("https://localhost:7178/api/v1/Employees", requestOptions)
                    .then((response) => response.json())
                    .then((result) => {
                        if (result.success) {
                            alert("Thêm mới thành công!");
                            const newPageUrl = `http://127.0.0.1:5500/Front-end/layout/employee.html`;
                            window.location.href = newPageUrl;
                        } else {
                            let popup = document.querySelector("#popup");
                            popup.querySelector(".popup-header").firstElementChild.innerHTML = "Thông báo lỗi"
                            let errors = ``;
                            if (result.errors.length > 0) {
                                for (const error of result.errors) {
                                    errors += `<li>${error}</li>`;
                                }
                            }
                            popup.querySelector(".popup-body").innerHTML = `<p>Lỗi thêm mới nhân viên</p>`;
                            popup.style.display = "block";
                        }
                    })
                    .catch((error) => {
                        console.error(error)
                    });
            } else {
                const myHeaders = new Headers();
                myHeaders.append("Content-Type", "application/json");

                const raw = JSON.stringify(this.data);

                const requestOptions = {
                    method: "POST",
                    headers: myHeaders,
                    body: raw,
                    redirect: "follow"
                };
                debugger;
                fetch("https://localhost:7178/api/v1/Employees/UpdateEmployee", requestOptions)
                    .then((response) => response.json())
                    .then((result) => {
                        if (result.success) {
                            alert("Cập nhật thành công!");
                            const newPageUrl = `http://127.0.0.1:5500/Front-end/layout/employee.html`;
                            window.location.href = newPageUrl;
                        } else {
                            let popup = document.querySelector("#popup");
                            popup.querySelector(".popup-header").firstElementChild.innerHTML = "Thông báo lỗi"
                            let errors = ``;
                            if (result.errors.length > 0) {
                                for (const error of result.errors) {
                                    errors += `<li>${error}</li>`;
                                }
                            }
                            popup.querySelector(".popup-body").innerHTML = `<p>Lỗi sửa mới nhân viên</p>`;
                            popup.style.display = "block";
                        }
                    })
                    .catch((error) => {
                        console.error(error)
                    });
            }
        }
    }


    /**
     * lấy dữ liệu từ form
     * Author: Ngô Minh Hiếu (5-8-2024)
     */
    getDataEmployee(form) {
        // Tạo một đối tượng FormData từ biểu mẫu
        const formData = new FormData(form);

        // Tạo đối tượng JSON từ FormData
        const data = {};
        if (this.currentId !== "0") {
            data['employeeId'] = this.currentId;
        }
        formData.forEach((value, key) => {
            // Xử lý radio buttons
            if (key === 'sex') {
                if (value === 'male') {
                    data[key] = 1;
                } else if (value === 'female') {
                    data[key] = 2;
                } else {
                    data[key] = 3;
                }
            } else {
                // Xử lý các giá trị khác
                data[key] = value || null;
            }
        });

        this.data = data;
        console.log(JSON.stringify(this.data, null, 2));
    }
}