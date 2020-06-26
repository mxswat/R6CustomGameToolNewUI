function animationTabs() {
    var tabs = document.getElementsByClassName("players")[0];
    // First by default is active
    tabs.querySelector(".playerlabel").classList.add("active");
    var selector = document
      .getElementsByClassName("tabs")[0]
      .getElementsByClassName("playerlabel").length;

    var activeItem = tabs.querySelector(".active");
    var activeWidth = activeItem.clientWidth;
    document.getElementsByClassName("selector")[0].style.left =
      activeItem.offsetLeft + "px";
    document.getElementsByClassName("selector")[0].style.width =
      activeWidth + "px";

    var links = document
      .getElementsByClassName("players")[0]
      .getElementsByClassName("playerlabel");

    for (let i = 0; i < links.length; i++) {
      const link = links[i];
      link.addEventListener("click", function(e) {
        document
          .getElementsByClassName("tabs")[0]
          .getElementsByClassName("active")[0]
          .classList.remove("active");
        this.classList.add("active");
        var activeWidth = this.clientWidth;
        var itemPos = this.offsetLeft;
        document.getElementsByClassName("selector")[0].style.left =
          itemPos + "px";
        document.getElementsByClassName("selector")[0].style.width =
          activeWidth + "px";
      });
    }
}

export default animationTabs;