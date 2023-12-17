import{i as qe,f as Je,h as _e,j as Ke,s as Qe,n as pe,r as Xe,o as Ye,b as xe}from"../chunks/scheduler.e3374dcb.js";import{p as $e,t as et,b as tt,d as lt,S as st,i as nt,g as v,s as I,h as f,j as w,y as M,c as D,f as p,k as o,l as Ve,a as F,x as c,z as q,A as Ze,m as J,n as K,o as ce,e as Fe}from"../chunks/index.fa8ba58e.js";import{f as Y}from"../chunks/fetch-refresh.d3a28ad6.js";function He(t,e){const s=e.token={};function l(n,i,a,d){if(e.token!==s)return;e.resolved=d;let _=e.ctx;a!==void 0&&(_=_.slice(),_[a]=d);const h=n&&(e.current=n)(_);let k=!1;e.block&&(e.blocks?e.blocks.forEach((C,T)=>{T!==i&&C&&($e(),et(C,1,1,()=>{e.blocks[T]===C&&(e.blocks[T]=null)}),tt())}):e.block.d(1),h.c(),lt(h,1),h.m(e.mount(),e.anchor),k=!0),e.block=h,e.blocks&&(e.blocks[i]=h),k&&Ke()}if(qe(t)){const n=Je();if(t.then(i=>{_e(n),l(e.then,1,e.value,i),_e(null)},i=>{if(_e(n),l(e.catch,2,e.error,i),_e(null),!e.hasCatch)throw i}),e.current!==e.pending)return l(e.pending,0),!0}else{if(e.current!==e.then)return l(e.then,1,e.value,t),!0;e.resolved=t}}function at(t,e,s){const l=e.slice(),{resolved:n}=t;t.current===t.then&&(l[t.value]=n),t.current===t.catch&&(l[t.error]=n),t.block.p(l,s)}function be(t){return(t==null?void 0:t.length)!==void 0?t:Array.from(t)}function Ae(t,e,s){const l=t.slice();return l[24]=e[s],l}function Ge(t,e,s){const l=t.slice();return l[28]=e[s],l}function ot(t){let e,s,l=t[31].message+"",n;return{c(){e=v("p"),s=J("Error: "),n=J(l),this.h()},l(i){e=f(i,"P",{style:!0,class:!0});var a=w(e);s=K(a,"Error: "),n=K(a,l),a.forEach(p),this.h()},h(){Ve(e,"color","red"),o(e,"class","svelte-bi1u9v")},m(i,a){F(i,e,a),c(e,s),c(e,n)},p(i,a){a[0]&1&&l!==(l=i[31].message+"")&&ce(n,l)},d(i){i&&p(e)}}}function ct(t){let e,s=be(t[27]),l=[];for(let n=0;n<s.length;n+=1)l[n]=ze(Ge(t,s,n));return{c(){for(let n=0;n<l.length;n+=1)l[n].c();e=Fe()},l(n){for(let i=0;i<l.length;i+=1)l[i].l(n);e=Fe()},m(n,i){for(let a=0;a<l.length;a+=1)l[a]&&l[a].m(n,i);F(n,e,i)},p(n,i){if(i[0]&2181){s=be(n[27]);let a;for(a=0;a<s.length;a+=1){const d=Ge(n,s,a);l[a]?l[a].p(d,i):(l[a]=ze(d),l[a].c(),l[a].m(e.parentNode,e))}for(;a<l.length;a+=1)l[a].d(1);l.length=s.length}},d(n){n&&p(e),Ze(l,n)}}}function ze(t){let e,s,l=t[28].name[0]+"",n,i,a,d=t[28].name+"",_,h,k,C,T;function E(){return t[17](t[28])}return{c(){e=v("button"),s=v("div"),n=J(l),i=I(),a=v("div"),_=J(d),h=I(),this.h()},l(b){e=f(b,"BUTTON",{class:!0,"aria-current":!0});var u=w(e);s=f(u,"DIV",{class:!0});var O=w(s);n=K(O,l),O.forEach(p),i=D(u),a=f(u,"DIV",{class:!0});var U=w(a);_=K(U,d),U.forEach(p),h=D(u),u.forEach(p),this.h()},h(){o(s,"class","chat-list__icon svelte-bi1u9v"),o(a,"class","chat-list__name svelte-bi1u9v"),o(e,"class","chat-list__item svelte-bi1u9v"),o(e,"aria-current",k=t[2]===t[28])},m(b,u){F(b,e,u),c(e,s),c(s,n),c(e,i),c(e,a),c(a,_),c(e,h),C||(T=q(e,"click",E),C=!0)},p(b,u){t=b,u[0]&1&&l!==(l=t[28].name[0]+"")&&ce(n,l),u[0]&1&&d!==(d=t[28].name+"")&&ce(_,d),u[0]&5&&k!==(k=t[2]===t[28])&&o(e,"aria-current",k)},d(b){b&&p(e),C=!1,T()}}}function it(t){let e,s="Loading...";return{c(){e=v("p"),e.textContent=s,this.h()},l(l){e=f(l,"P",{class:!0,"data-svelte-h":!0}),M(e)!=="svelte-qdsr2u"&&(e.textContent=s),this.h()},h(){o(e,"class","svelte-bi1u9v")},m(l,n){F(l,e,n)},p:pe,d(l){l&&p(e)}}}function Re(t){let e,s,l=t[24].text+"",n,i,a,d=t[24].user.name+"",_,h,k=new Date(Date.parse(t[24].dateTime)).toLocaleString()+"",C,T,E;return{c(){e=v("div"),s=v("div"),n=J(l),i=I(),a=v("div"),_=J(d),h=J(", "),C=J(k),T=I(),this.h()},l(b){e=f(b,"DIV",{class:!0});var u=w(e);s=f(u,"DIV",{class:!0});var O=w(s);n=K(O,l),O.forEach(p),i=D(u),a=f(u,"DIV",{class:!0});var U=w(a);_=K(U,d),h=K(U,", "),C=K(U,k),U.forEach(p),T=D(u),u.forEach(p),this.h()},h(){o(s,"class","message-text svelte-bi1u9v"),o(a,"class","message-info svelte-bi1u9v"),o(e,"class",E="message "+(t[24].user.id===t[1]?"message-self":"")+" svelte-bi1u9v")},m(b,u){F(b,e,u),c(e,s),c(s,n),c(e,i),c(e,a),c(a,_),c(a,h),c(a,C),c(e,T)},p(b,u){u[0]&8&&l!==(l=b[24].text+"")&&ce(n,l),u[0]&8&&d!==(d=b[24].user.name+"")&&ce(_,d),u[0]&8&&k!==(k=new Date(Date.parse(b[24].dateTime)).toLocaleString()+"")&&ce(C,k),u[0]&10&&E!==(E="message "+(b[24].user.id===b[1]?"message-self":"")+" svelte-bi1u9v")&&o(e,"class",E)},d(b){b&&p(e)}}}function We(t){let e,s,l,n,i='<svg width="30px" height="30px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg" class="svelte-bi1u9v"><path fill-rule="evenodd" clip-rule="evenodd" d="M3.3938 2.20468C3.70395 1.96828 4.12324 1.93374 4.4679 2.1162L21.4679 11.1162C21.7953 11.2895 22 11.6296 22 12C22 12.3704 21.7953 12.7105 21.4679 12.8838L4.4679 21.8838C4.12324 22.0662 3.70395 22.0317 3.3938 21.7953C3.08365 21.5589 2.93922 21.1637 3.02382 20.7831L4.97561 12L3.02382 3.21692C2.93922 2.83623 3.08365 2.44109 3.3938 2.20468ZM6.80218 13L5.44596 19.103L16.9739 13H6.80218ZM16.9739 11H6.80218L5.44596 4.89699L16.9739 11Z" fill="#000000" class="svelte-bi1u9v"></path></svg>',a,d;return{c(){e=v("form"),s=v("input"),l=I(),n=v("button"),n.innerHTML=i,this.h()},l(_){e=f(_,"FORM",{class:!0});var h=w(e);s=f(h,"INPUT",{class:!0,type:!0,size:!0,placeholder:!0}),l=D(h),n=f(h,"BUTTON",{class:!0,type:!0,"data-svelte-h":!0}),M(n)!=="svelte-yk5g3u"&&(n.innerHTML=i),h.forEach(p),this.h()},h(){o(s,"class","chat-input__input svelte-bi1u9v"),o(s,"type","text"),o(s,"size","1"),o(s,"placeholder","Введите сообщение..."),o(n,"class","chat-input__btn svelte-bi1u9v"),o(n,"type","submit"),o(e,"class","chat-input svelte-bi1u9v")},m(_,h){F(_,e,h),c(e,s),t[21](s),c(e,l),c(e,n),a||(d=q(e,"submit",t[12]),a=!0)},p:pe,d(_){_&&p(e),t[21](null),a=!1,d()}}}function rt(t){let e,s,l,n,i="Создание чата",a,d,_,h,k="Создать",C,T,E,b,u,O,U="Добавление кого нибудь",ie,A,re,j,ue="Добавить",$,ee,B,N,r,m,te="Чаты",me,he,ge,z,Oe="Создать чат",Ce,R,Le="Выйти",de,ke,P,H,W,Ne="≡",ye,Q,Se="Чат",we,Z,Ue="+",Te,X,Ie,De,Me,S={ctx:t,current:null,token:null,hasCatch:!0,pending:it,then:ct,catch:ot,value:27,error:31};He(he=t[0],S);let le=be(t[3]),x=[];for(let g=0;g<le.length;g+=1)x[g]=Re(Ae(t,le,g));let L=t[2]&&We(t);return{c(){e=v("div"),s=v("div"),l=v("form"),n=v("h2"),n.textContent=i,a=I(),d=v("input"),_=I(),h=v("button"),h.textContent=k,T=I(),E=v("div"),b=v("div"),u=v("form"),O=v("h2"),O.textContent=U,ie=I(),A=v("input"),re=I(),j=v("button"),j.textContent=ue,ee=I(),B=v("div"),N=v("div"),r=v("div"),m=v("h2"),m.textContent=te,me=I(),S.block.c(),ge=I(),z=v("button"),z.textContent=Oe,Ce=I(),R=v("button"),R.textContent=Le,ke=I(),P=v("div"),H=v("div"),W=v("button"),W.textContent=Ne,ye=I(),Q=v("h2"),Q.textContent=Se,we=I(),Z=v("button"),Z.textContent=Ue,Te=I(),X=v("div");for(let g=0;g<x.length;g+=1)x[g].c();Ie=I(),L&&L.c(),this.h()},l(g){e=f(g,"DIV",{class:!0,style:!0});var V=w(e);s=f(V,"DIV",{class:!0});var y=w(s);l=f(y,"FORM",{class:!0});var G=w(l);n=f(G,"H2",{class:!0,"data-svelte-h":!0}),M(n)!=="svelte-63l9rp"&&(n.textContent=i),a=D(G),d=f(G,"INPUT",{type:!0,class:!0,placeholder:!0}),_=D(G),h=f(G,"BUTTON",{type:!0,class:!0,style:!0,"data-svelte-h":!0}),M(h)!=="svelte-70sbn9"&&(h.textContent=k),G.forEach(p),y.forEach(p),V.forEach(p),T=D(g),E=f(g,"DIV",{class:!0,style:!0});var je=w(E);b=f(je,"DIV",{class:!0});var Be=w(b);u=f(Be,"FORM",{class:!0});var se=w(u);O=f(se,"H2",{class:!0,"data-svelte-h":!0}),M(O)!=="svelte-1bx5scp"&&(O.textContent=U),ie=D(se),A=f(se,"INPUT",{type:!0,class:!0,placeholder:!0}),re=D(se),j=f(se,"BUTTON",{type:!0,class:!0,style:!0,"data-svelte-h":!0}),M(j)!=="svelte-1vbzwa6"&&(j.textContent=ue),se.forEach(p),Be.forEach(p),je.forEach(p),ee=D(g),B=f(g,"DIV",{class:!0});var ve=w(B);N=f(ve,"DIV",{class:!0,style:!0});var ne=w(N);r=f(ne,"DIV",{class:!0});var fe=w(r);m=f(fe,"H2",{class:!0,"data-svelte-h":!0}),M(m)!=="svelte-ikowhi"&&(m.textContent=te),me=D(fe),S.block.l(fe),fe.forEach(p),ge=D(ne),z=f(ne,"BUTTON",{class:!0,"data-svelte-h":!0}),M(z)!=="svelte-hhx7w9"&&(z.textContent=Oe),Ce=D(ne),R=f(ne,"BUTTON",{class:!0,"data-svelte-h":!0}),M(R)!=="svelte-hpsjps"&&(R.textContent=Le),ne.forEach(p),ke=D(ve),P=f(ve,"DIV",{class:!0});var ae=w(P);H=f(ae,"DIV",{class:!0});var oe=w(H);W=f(oe,"BUTTON",{class:!0,"data-svelte-h":!0}),M(W)!=="svelte-8sxf5b"&&(W.textContent=Ne),ye=D(oe),Q=f(oe,"H2",{class:!0,"data-svelte-h":!0}),M(Q)!=="svelte-2d8y5b"&&(Q.textContent=Se),we=D(oe),Z=f(oe,"BUTTON",{class:!0,"data-svelte-h":!0}),M(Z)!=="svelte-rljtic"&&(Z.textContent=Ue),oe.forEach(p),Te=D(ae),X=f(ae,"DIV",{class:!0});var Pe=w(X);for(let Ee=0;Ee<x.length;Ee+=1)x[Ee].l(Pe);Pe.forEach(p),Ie=D(ae),L&&L.l(ae),ae.forEach(p),ve.forEach(p),this.h()},h(){o(n,"class","svelte-bi1u9v"),o(d,"type","text"),o(d,"class","create-chat-modal__text svelte-bi1u9v"),o(d,"placeholder","Название чата..."),o(h,"type","submit"),o(h,"class","btn svelte-bi1u9v"),Ve(h,"width","100%"),o(l,"class","svelte-bi1u9v"),o(s,"class","create-chat-modal__container svelte-bi1u9v"),o(e,"class","create-chat-modal svelte-bi1u9v"),o(e,"style",C=t[8]?"display: flex;":"display: none;"),o(O,"class","svelte-bi1u9v"),o(A,"type","text"),o(A,"class","create-chat-modal__text svelte-bi1u9v"),o(A,"placeholder","Имя..."),o(j,"type","submit"),o(j,"class","btn svelte-bi1u9v"),Ve(j,"width","100%"),o(u,"class","svelte-bi1u9v"),o(b,"class","create-chat-modal__container svelte-bi1u9v"),o(E,"class","create-chat-modal svelte-bi1u9v"),o(E,"style",$=t[9]?"display: flex;":"display: none;"),o(m,"class","svelte-bi1u9v"),o(r,"class","chat-list svelte-bi1u9v"),o(z,"class","btn create-chat-btn svelte-bi1u9v"),o(R,"class","btn logout-btn svelte-bi1u9v"),o(N,"class","left-panel svelte-bi1u9v"),o(N,"style",de=t[7]==1?"display: flex;":""),o(W,"class","btn chat-drop svelte-bi1u9v"),o(Q,"class","svelte-bi1u9v"),o(Z,"class","btn svelte-bi1u9v"),o(H,"class","chat-title svelte-bi1u9v"),o(X,"class","messages svelte-bi1u9v"),o(P,"class","chat svelte-bi1u9v"),o(B,"class","app svelte-bi1u9v")},m(g,V){F(g,e,V),c(e,s),c(s,l),c(l,n),c(l,a),c(l,d),t[15](d),c(l,_),c(l,h),F(g,T,V),F(g,E,V),c(E,b),c(b,u),c(u,O),c(u,ie),c(u,A),t[16](A),c(u,re),c(u,j),F(g,ee,V),F(g,B,V),c(B,N),c(N,r),c(r,m),c(r,me),S.block.m(r,S.anchor=null),S.mount=()=>r,S.anchor=null,c(N,ge),c(N,z),c(N,Ce),c(N,R),c(B,ke),c(B,P),c(P,H),c(H,W),c(H,ye),c(H,Q),c(H,we),c(H,Z),c(P,Te),c(P,X);for(let y=0;y<x.length;y+=1)x[y]&&x[y].m(X,null);c(P,Ie),L&&L.m(P,null),De||(Me=[q(l,"submit",t[14]),q(u,"submit",t[10]),q(z,"click",t[18]),q(R,"click",t[13]),q(W,"click",t[19]),q(Z,"click",t[20])],De=!0)},p(g,V){if(t=g,V[0]&256&&C!==(C=t[8]?"display: flex;":"display: none;")&&o(e,"style",C),V[0]&512&&$!==($=t[9]?"display: flex;":"display: none;")&&o(E,"style",$),S.ctx=t,V[0]&1&&he!==(he=t[0])&&He(he,S)||at(S,t,V),V[0]&128&&de!==(de=t[7]==1?"display: flex;":"")&&o(N,"style",de),V[0]&10){le=be(t[3]);let y;for(y=0;y<le.length;y+=1){const G=Ae(t,le,y);x[y]?x[y].p(G,V):(x[y]=Re(G),x[y].c(),x[y].m(X,null))}for(;y<x.length;y+=1)x[y].d(1);x.length=le.length}t[2]?L?L.p(t,V):(L=We(t),L.c(),L.m(P,null)):L&&(L.d(1),L=null)},i:pe,o:pe,d(g){g&&(p(e),p(T),p(E),p(ee),p(B)),t[15](null),t[16](null),S.block.d(),S.token=null,S=null,Ze(x,g),L&&L.d(),De=!1,Xe(Me)}}}function ut(t,e,s){let l=u(),n,i,a=[],d,_,h,k=0,C=!1,T=!1;Ye(async()=>{const r=await Y("/User.Get",{credentials:"same-origin"});if(r.ok){const m=await r.json();s(1,n=m.userId),E()}else window.location.href="/login"});function E(){let r=new WebSocket("wss://uovh.ru/ws");r.onmessage=function(m){let te=JSON.parse(m.data);console.log(te),console.log("Selected: "),console.log(i.id),te.chatId===i.id&&(console.log("push"),s(3,a=[te,...a]),console.log(a))},r.onclose=function(m){console.log("Socket is closed. Reconnect will be attempted in 1 second.",m.reason),setTimeout(function(){connect()},1e3)},r.onerror=function(m){console.error("Socket encountered error: ",m.message,"Closing socket"),r.close()}}async function b(){let r=new FormData,m=h.value;s(6,h.value="",h),s(9,T=!1),r.append("chatId",i.id),r.append("name",m),await Y("/Chat.AddUserToChatByName",{credentials:"same-origin",method:"POST",body:r})}async function u(){const m=await(await Y("/Chat.GetUserChats",{credentials:"same-origin"})).json();return console.log(m),m}async function O(){let m=await(await Y(`/Chat.GetChatMessages?chatId=${i.id}`,{credentials:"same-origin"})).json();m.reverse(),s(3,a=m),console.log(a)}async function U(){let r=new FormData,m=d.value;s(4,d.value="",d),r.append("chatId",i.id),r.append("text",m),await Y("/Chat.SendChatMessage",{credentials:"same-origin",method:"POST",body:r})}async function ie(){await Y("/Auth/Logout",{credentials:"same-origin"}),window.location.href="/login"}async function A(){s(8,C=!1);let r=_.value;s(5,_.value="",_),await Y("/Chat.CreateChat",{method:"POST",credentials:"same-origin",headers:{"Content-Type":"application/json"},body:JSON.stringify({name:r})}),s(0,l=u())}function re(r){xe[r?"unshift":"push"](()=>{_=r,s(5,_)})}function j(r){xe[r?"unshift":"push"](()=>{h=r,s(6,h)})}const ue=r=>{s(2,i=r),O(),s(7,k=0)},$=()=>s(8,C=!0),ee=()=>s(7,k=1),B=()=>s(9,T=!0);function N(r){xe[r?"unshift":"push"](()=>{d=r,s(4,d)})}return[l,n,i,a,d,_,h,k,C,T,b,O,U,ie,A,re,j,ue,$,ee,B,N]}class ft extends st{constructor(e){super(),nt(this,e,ut,rt,Qe,{},null,[-1,-1])}}export{ft as component};