async function f(e,a){let t=await fetch(e,a);return t.status===401&&(await fetch("/Auth/Refresh",{credentials:"same-origin"}),t=await fetch(e,a)),t}export{f};
