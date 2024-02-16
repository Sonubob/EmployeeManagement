
// Get the modal
var modal = document.getElementById("myModal");

// Get the button that opens and closes the modal
var btn = document.getElementById("logout");
var loginbtn = document.getElementById("redirectLogin");

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// When the user clicks on the button, open the modal
btn.onclick = function () {
    modal.style.display = "block";
}
//when user clicks on redirect to login, close modal and redirect
loginbtn.onclick = function () {
    modal.style.display = "none";

    $.get("/Account/Logout", function (data) {
        window.location = data.url;
    });
}
// When the user clicks on <span> (x), close the modal
span.onclick = function () {
    modal.style.display = "none";

    $.get("/Account/Logout", function (data) {
        window.location = data.url;
    });
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
        $.get("/Account/Logout", function (data) {
            window.location = data.url;
        });
    }
}