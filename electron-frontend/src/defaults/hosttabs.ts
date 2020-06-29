// TODO REMIND TO CHECK SIZE OOF selector WHEN PLAYER LIST IS UPDATED

function animationTabs() {
    const tabs: any = document.getElementsByClassName("players")[0];
    // First by default is active
    tabs.querySelector(".playerlabel").classList.add("active");
    const selector = document
      .getElementsByClassName("tabs")[0]
      .getElementsByClassName("playerlabel").length;

    const activeItem = tabs.querySelector(".active");
    const activeWidth = activeItem.clientWidth;
    (document.getElementsByClassName("selector")[0] as any).style.left =
      activeItem.offsetLeft + "px";
      (document.getElementsByClassName("selector")[0] as any).style.width =
      activeWidth + "px";

    const links = document
      .getElementsByClassName("players")[0]
      .getElementsByClassName("playerlabel");

    for (let i = 0; i < links.length; i++) {
      const link = links[i];
      link.addEventListener("click", function(e: any) {
        document
          .getElementsByClassName("tabs")[0]
          .getElementsByClassName("active")[0]
          .classList.remove("active");
        e.toElement.classList.add("active");
        const activeWidth = e.toElement.clientWidth;
        const itemPos = e.toElement.offsetLeft;
        (document.getElementsByClassName("selector")[0] as any).style.left =
          itemPos + "px";
        (document.getElementsByClassName("selector")[0] as any).style.width =
          activeWidth + "px";
      });
    }
}

export default animationTabs;