
export function setTheme(val) {
  localStorage.setItem("theme", val);
}

export function getTheme() {
  let l = localStorage.getItem("theme");
  if (l == null || l == undefined) {
    l = "0";
    localStorage.setItem("theme", l);
  }
  return l;
}

export function setupTheme() {
  let x = localStorage.getItem("theme").toString();
  switch (x) {
    case null:
    case undefined:
    case "0":
      document.documentElement.className = "theme-light";
      break;
    case "1":
      document.documentElement.className = "theme-dark";
      break;
  }
}

export function fadeIn(elem, ms) {
  if (ms == undefined) ms = 160;
  if (!elem) return;
  if (elem.style == undefined) elem.style = {};
  elem.style.opacity = 0;
  elem.style.filter = "alpha(opacity=0)";
  elem.style.display = "inline-block";
  elem.style.visibility = "visible";
  if (ms) {
    let opacity = 0;
    let timer = setInterval(function () {
      opacity += 50 / ms;
      if (opacity >= 1) {
        clearInterval(timer);
        opacity = 1;
      }
      elem.style.opacity = opacity;
      elem.style.filter = "alpha(opacity=" + opacity * 100 + ")";
    }, 50);
  } else {
    elem.style.opacity = 1;
    elem.style.filter = "alpha(opacity=1)";
  }
}

export function fadeOut(elem, ms) {
  if (!elem) return;
  if (elem.style == undefined) elem.style = {};

  if (ms) {
    let opacity = 1;
    let timer = setInterval(function () {
      opacity -= 50 / ms;
      if (opacity <= 0) {
        clearInterval(timer);
        opacity = 0;
        elem.style.display = "none";
        elem.style.visibility = "hidden";
      }
      elem.style.opacity = opacity;
      elem.style.filter = "alpha(opacity=" + opacity * 100 + ")";
    }, 50);
  } else {
    elem.style.opacity = 0;
    elem.style.filter = "alpha(opacity=0)";
    elem.style.display = "none";
    elem.style.visibility = "hidden";
  }
}

export function slideUp(element, duration, finalheight) {
  if (element.style == undefined) element.style = {};
  let s = element.style;
  s.height = "0px";
  let y = 0;
  let framerate = 10;
  let one_second = 1000;
  let interval = (one_second * duration) / framerate;
  let totalframes = (one_second * duration) / interval;
  let heightincrement = finalheight / totalframes;
  let tweenUp = function () {
    y -= heightincrement;
    s.height = y + "px";
    if (y < 0) {
      setTimeout(tweenUp, interval);
    }
  };
  tweenUp();
}

export function slideDown(element, duration, finalheight) {
  if (element.style == undefined) element.style = {};
  let s = element.style;
  s.height = "0px";
  let y = 0;
  let framerate = 10;
  let one_second = 1000;
  let interval = (one_second * duration) / framerate;
  let totalframes = (one_second * duration) / interval;
  let heightincrement = finalheight / totalframes;
  let tweenDown = function () {
    y += heightincrement;
    s.height = y + "px";
    if (y < finalheight) {
      setTimeout(tweenDown, interval);
    }
  };
  tweenDown();
}

