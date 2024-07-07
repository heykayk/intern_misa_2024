// Sample data for employees
const employees = [
    {
        code: 'EMP001',
        name: 'Phạm Văn A',
        gender: 'Nam',
        dob: '02/02/2002',
        email: '123@gmail.com',
        address: 'Hà Đông, hà nội',
    },
    {
        code: 'EMP002',
        name: 'Phạm Văn B',
        gender: 'Nữ',
        dob: '02/02/2002',
        email: '123@gmail.com',
        address: 'Hà Đông, hà nội',
    },
    {
        code: 'EMP003',
        name: 'Phạm Văn C',
        gender: 'Nam',
        dob: '02/02/2002',
        email: '123@gmail.com',
        address: 'Hà Đông, hà nội',
    },
    {
        code: 'EMP004',
        name: 'Phạm Văn D',
        gender: 'Nam',
        dob: '02/02/2002',
        email: '123@gmail.com',
        address: 'Hà Đông, hà nội',
    },
    {
        code: 'EMP005',
        name: 'Phạm Văn E',
        gender: 'Nam',
        dob: '02/02/2002',
        email: '123@gmail.com',
        address: 'Hà Đông, hà nội',
    },
    {
        code: 'EMP006',
        name: 'Phạm Văn F',
        gender: 'Nam',
        dob: '02/02/2002',
        email: '123@gmail.com',
        address: 'Hà Đông, hà nội',
    },
    
    {
        code: 'EMP006',
        name: 'Phạm Văn F',
        gender: 'Nam',
        dob: '02/02/2002',
        email: '123@gmail.com',
        address: 'Hà Đông, hà nội',
    },
    
    {
        code: 'EMP006',
        name: 'Phạm Văn F',
        gender: 'Nam',
        dob: '02/02/2002',
        email: '123@gmail.com',
        address: 'Hà Đông, hà nội',
    },
    
    {
        code: 'EMP006',
        name: 'Phạm Văn F',
        gender: 'Nam',
        dob: '02/02/2002',
        email: '123@gmail.com',
        address: 'Hà Đông, hà nội',
    },
    
    {
        code: 'EMP006',
        name: 'Phạm Văn F',
        gender: 'Nam',
        dob: '02/02/2002',
        email: '123@gmail.com',
        address: 'Hà Đông, hà nội',
    },
    
    {
        code: 'EMP006',
        name: 'Phạm Văn F',
        gender: 'Nam',
        dob: '02/02/2002',
        email: '123@gmail.com',
        address: 'Hà Đông, hà nội',
    },
];

function openModal() {
    document.getElementById('employeeModal').style.display = 'flex';
}

function closeModal() {
    document.getElementById('employeeModal').style.display = 'none';
}

// Function to render the employee table
function renderTable() {
    const tableBody = document.getElementById('employeeTableBody');
    tableBody.innerHTML = '';
    employees.forEach((employee, index) => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${index + 1}</td>
            <td>${employee.code}</td>
            <td>${employee.name}</td>
            <td>${employee.gender}</td>
            <td>${employee.dob}</td>
            <td>${employee.email}</td>
            <td class="address">
                ${employee.address}
            </td>
        `;
        tableBody.appendChild(row);
    });
}

// Function to open modal
function openModal(employeeIndex = null) {
    document.getElementById('employeeModal').style.display = 'flex';
    if (employeeIndex !== null) {
        const employee = employees[employeeIndex];
        document.getElementById('employeeId').value = employeeIndex;
        document.getElementById('employeeName').value = employee.name;
        // Set other form fields accordingly
    }
}

// Function to close modal
function closeModal() {
    document.getElementById('employeeModal').style.display = 'none';
    document.getElementById('employeeForm').reset();
}

renderTable();
