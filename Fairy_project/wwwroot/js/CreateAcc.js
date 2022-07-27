const M_permissions = document.querySelector('#M_permissions');
const memAccount = document.querySelector('#memAccount');
const manufacturesAcc = document.querySelector('#manufacturesAcc');
const MF_permissions = document.querySelector('#MF_permissions')

memAccount.addEventListener('input', event => {
    const data = event.target.value;
    M_permissions.value = data;
});

manufacturesAcc.addEventListener('input', event => {
    const data = event.target.value;
    MF_permissions.value = data;
})


$('#login').click(function () {
    sessionStorage.setItem('acount', $('#ac').val())
})
