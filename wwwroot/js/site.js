var storedTheme = sessionStorage.getItem("theme_input")
var newTheme = "";
if (storedTheme === "Sams theme") {
    newTheme = "Sam"
    document.getElementById("CssTheme").setAttribute("href", "/css/SamsTheme.css")
}
else if (storedTheme != "Sams theme") {
    newTheme = "Dark"
}
else {
    newTheme = ""
}
localStorage.setItem("Theme", newTheme)
console.log(newTheme)