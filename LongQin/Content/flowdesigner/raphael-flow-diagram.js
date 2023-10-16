(function ($) {
    var Diagram = {
        createNew: function (pager) {
            var diagram = {};
            diagram.rectarr = {}; //节点集合
            diagram.patharr = {}; //连线集合
            diagram.config = {
                editable: true,
                lineHeight: 15,
                basePath: "",
                rect: {
                    attr: {
                        x: 300,
                        y: 100,
                        width: 100,
                        height: 50,
                        r: 5,
                        fill: "90-#fff-#C0C0C0",
                        stroke: "#000",
                        "stroke-width": 1
                    },
                    data: { //节点属性数据
                        id: 0,
                        name: '',
                        recttype: 0,
                        mainmenu: '',
                        copymenu: '',
                        cooperation: 0,
                        virtual: 0,
                        dept: '',
                        position: '',
                        userid: '',
                        username: '',
                        remark: ''
                    },
                    text: {
                        text: "新建节点",
                        "font-size": 12
                    },
                    margin: 5
                },
                path: {
                    attr: {
                        path: {
                            path: "M10 10L100 100",
                            stroke: "#808080",
                            fill: "none",
                            "stroke-width": 2
                        },
                        arrow: {
                            path: "M10 10L10 10",
                            stroke: "#808080",
                            fill: "#808080",
                            "stroke-width": 2,
                            radius: 4
                        },
                        fromDot: {
                            width: 5,
                            height: 5,
                            stroke: "#fff",
                            fill: "#000",
                            cursor: "move",
                            "stroke-width": 2
                        },
                        toDot: {
                            width: 5,
                            height: 5,
                            stroke: "#fff",
                            fill: "#000",
                            cursor: "move",
                            "stroke-width": 2
                        },
                        bigDot: {
                            width: 5,
                            height: 5,
                            stroke: "#fff",
                            fill: "#000",
                            cursor: "move",
                            "stroke-width": 2
                        },
                        smallDot: {
                            width: 5,
                            height: 5,
                            stroke: "#fff",
                            fill: "#000",
                            cursor: "move",
                            "stroke-width": 3
                        }
                    },
                    data: { //连线属性数据
                        id: 0,
                        name: '',
                        operatortext: '', //条件符号，如‘=’
                        condition: '', //条件值
                        remark: ''
                    },
                    text: {
                        text: "",
                        background: "#000",
                        "font-size": 12
                    },
                    textPos: {
                        x: 0,
                        y: -10
                    }
                },
                restore: ""
            };
            diagram.util = { //方法集
                isLine: function (g, f, e) {
                    var d, c;
                    if ((g.x - e.x) == 0) {
                        d = 1;
                    } else {
                        d = (g.y - e.y) / (g.x - e.x);
                    }
                    c = (f.x - e.x) * d + e.y;
                    if ((f.y - c) < 10 && (f.y - c) > -10) {
                        f.y = c;
                        return true;
                    }
                    return false;
                },
                center: function (d, c) {
                    return {
                        x: (d.x - c.x) / 2 + c.x,
                        y: (d.y - c.y) / 2 + c.y
                    }
                },
                nextId: (function () {
                    var c = 0;
                    return function () {
                        return ++c;
                    }
                })(),
                connPoint: function (j, d) {
                    var c = d,
                    e = {
                        x: j.x + j.width / 2,
                        y: j.y + j.height / 2
                    };
                    var l = (e.y - c.y) / (e.x - c.x);
                    l = isNaN(l) ? 0 : l;
                    var k = j.height / j.width;
                    var h = c.y < e.y ? -1 : 1,
                    f = c.x < e.x ? -1 : 1,
                    g,
                    i;
                    if (Math.abs(l) > k && h == -1) {
                        g = e.y - j.height / 2;
                        i = e.x + h * j.height / 2 / l;
                    } else {
                        if (Math.abs(l) > k && h == 1) {
                            g = e.y + j.height / 2;
                            i = e.x + h * j.height / 2 / l;
                        } else {
                            if (Math.abs(l) < k && f == -1) {
                                g = e.y + f * j.width / 2 * l;
                                i = e.x - j.width / 2;
                            } else {
                                if (Math.abs(l) < k && f == 1) {
                                    g = e.y + j.width / 2 * l;
                                    i = e.x + j.width / 2;
                                }
                            }
                        }
                    }
                    return {
                        x: i,
                        y: g
                    }
                },
                arrow: function (l, k, d) {
                    var g = Math.atan2(l.y - k.y, k.x - l.x) * (180 / Math.PI);
                    var h = k.x - d * Math.cos(g * (Math.PI / 180));
                    var f = k.y + d * Math.sin(g * (Math.PI / 180));
                    var e = h + d * Math.cos((g + 120) * (Math.PI / 180));
                    var j = f - d * Math.sin((g + 120) * (Math.PI / 180));
                    var c = h + d * Math.cos((g + 240) * (Math.PI / 180));
                    var i = f - d * Math.sin((g + 240) * (Math.PI / 180));
                    return [k, {
                        x: e,
                        y: j
                    },
                    {
                        x: c,
                        y: i
                    }]
                }
            };
            diagram.rect = function (rect, curnode) {
                var u = this;
                var g = "rect" + diagram.util.nextId();
                var a;
                if (rect) {
                    a = $.extend(true, {}, diagram.config.rect, rect);
                }
                else {
                    a = diagram.config.rect;
                }
                var t = pager.rect(a.attr.x, a.attr.y, a.attr.width, a.attr.height, a.attr.r).attr(a.attr);
                if (curnode) {
                    t.attr("fill", "90-#fff-#0b92d5");
                    var n = pager.text(a.attr.x + 132, a.attr.y + 8, '[当前节点]').attr("fill", "rgb(20, 165, 236)");
                }
                var f = pager.text(a.attr.x + a.attr.width / 2, a.attr.y + a.attr.height / 2, a.text.text).attr(a.text);
                var q = {
                    x: a.attr.x - a.margin,
                    y: a.attr.y - a.margin,
                    width: a.attr.width + a.margin * 2,
                    height: a.attr.height + a.margin * 2
                };
                this.getBBox = function () {
                    return q;
                };
                this.getId = function () {
                    return g;
                };
                this.text = function () {
                    return f.attr("text");
                };
                this.attr = function (o) {
                    if (o) {
                        t.attr(o);
                    }
                };
            };
            diagram.path = function (begin, end, path) {
                var v = this;
                var i,
                t,
                f,
                y,
                w,
                x;
                var a;
                if (path) {
                    a = $.extend(true, {}, diagram.config.path, path);
                }
                else {
                    a = diagram.config.path;
                }
                var h = a.textPos;
                var g = "path" + diagram.util.nextId();
                //绘制连线上的点
                function p(G, H, D, L) {
                    var F = this,
                    M = G,
                    r, o = D,
                    O = L,
                    K, I, N = H;
                    switch (M) {
                        case "from":
                            r = pager.rect(H.x - a.attr.fromDot.width / 2, H.y - a.attr.fromDot.height / 2, a.attr.fromDot.width, a.attr.fromDot.height).attr(a.attr.fromDot);
                            break;
                        case "big":
                            r = pager.rect(H.x - a.attr.bigDot.width / 2, H.y - a.attr.bigDot.height / 2, a.attr.bigDot.width, a.attr.bigDot.height).attr(a.attr.bigDot);
                            break;
                        case "small":
                            r = pager.rect(H.x - a.attr.smallDot.width / 2, H.y - a.attr.smallDot.height / 2, a.attr.smallDot.width, a.attr.smallDot.height).attr(a.attr.smallDot);
                            break;
                        case "to":
                            r = pager.rect(H.x - a.attr.toDot.width / 2, H.y - a.attr.toDot.height / 2, a.attr.toDot.width, a.attr.toDot.height).attr(a.attr.toDot);
                            break
                    }
                    this.type = function (P) {
                        if (P) {
                            M = P
                        } else {
                            return M
                        }
                    };
                    this.node = function (P) {
                        if (P) {
                            r = P
                        } else {
                            return r
                        }
                    };
                    this.left = function (P) {
                        if (P) {
                            o = P
                        } else {
                            return o
                        }
                    };
                    this.right = function (P) {
                        if (P) {
                            O = P
                        } else {
                            return O
                        }
                    };
                    this.pos = function (P) {
                        if (P) {
                            N = P;
                            r.attr({
                                x: N.x - r.attr("width") / 2,
                                y: N.y - r.attr("height") / 2
                            });
                            return this
                        } else {
                            return N
                        }
                    };
                    this.moveTo = function (Q, T) {
                        this.pos({
                            x: Q,
                            y: T
                        });
                        switch (M) {
                            case "from":
                                if (O && O.right() && O.right().type() == "to") {
                                    O.right().pos(diagram.util.connPoint(end.getBBox(), N))
                                }
                                if (O && O.right()) {
                                    O.pos(diagram.util.center(N, O.right().pos()))
                                }
                                break;
                            case "big":
                                if (O && O.right() && O.right().type() == "to") {
                                    O.right().pos(diagram.util.connPoint(end.getBBox(), N))
                                }
                                if (o && o.left() && o.left().type() == "from") {
                                    o.left().pos(diagram.util.connPoint(begin.getBBox(), N))
                                }
                                if (O && O.right()) {
                                    O.pos(diagram.util.center(N, O.right().pos()))
                                }
                                if (o && o.left()) {
                                    o.pos(diagram.util.center(N, o.left().pos()))
                                }
                                var S = {
                                    x: N.x,
                                    y: N.y
                                };
                                if (diagram.util.isLine(o.left().pos(), S, O.right().pos())) {
                                    M = "small";
                                    r.attr(a.attr.smallDot);
                                    this.pos(S);
                                    var P = o;
                                    o.left().right(o.right());
                                    o = o.left();
                                    P.remove();
                                    var R = O;
                                    O.right().left(O.left());
                                    O = O.right();
                                    R.remove()
                                }
                                break;
                            case "small":
                                if (o && O && !diagram.util.isLine(o.pos(), {
                                    x: N.x,
                                    y: N.y
                                }, O.pos())) {
                                    M = "big";
                                    r.attr(a.attr.bigDot);
                                    var P = new p("small", diagram.util.center(o.pos(), N), o, o.right());
                                    o.right(P);
                                    o = P;
                                    var R = new p("small", diagram.util.center(O.pos(), N), O.left(), O);
                                    O.left(R);
                                    O = R
                                }
                                break;
                            case "to":
                                if (o && o.left() && o.left().type() == "from") {
                                    o.left().pos(diagram.util.connPoint(begin.getBBox(), N))
                                }
                                if (o && o.left()) {
                                    o.pos(diagram.util.center(N, o.left().pos()))
                                }
                                break
                        }
                        m()
                    }
                }
                function j() {
                    var D, C, E = begin.getBBox(), //起点属性
                    F = end.getBBox(), //终点属性
                    r,
                    o;
                    r = diagram.util.connPoint(E, {
                        x: F.x + F.width / 2,
                        y: F.y + F.height / 2
                    });
                    o = diagram.util.connPoint(F, r);
                    D = new p("from", r, null, new p("small", {
                        x: (r.x + o.x) / 2,
                        y: (r.y + o.y) / 2
                    }));
                    D.right().left(D);
                    C = new p("to", o, D.right(), null);
                    D.right().right(C);
                    this.toPathString = function () {
                        if (!D) {
                            return ""
                        }
                        var J = D,
                        I = "M" + J.pos().x + " " + J.pos().y,
                        H = "";
                        while (J.right()) {
                            J = J.right();
                            I += "L" + J.pos().x + " " + J.pos().y
                        }
                        var G = diagram.util.arrow(J.left().pos(), J.pos(), a.attr.arrow.radius);
                        H = "M" + G[0].x + " " + G[0].y + "L" + G[1].x + " " + G[1].y + "L" + G[2].x + " " + G[2].y + "z";
                        return [I, H]
                    };
                    this.midDot = function () {
                        var H = D.right(), G = D.right().right();
                        while (G.right() && G.right().right()) {
                            G = G.right().right();
                            H = H.right()
                        }
                        return H
                    };
                    this.hide = function () {
                        var G = D;
                        while (G) {
                            G.node().hide();
                            G = G.right()
                        }
                    };
                }
                i = pager.path(a.attr.path.path).attr(a.attr.path);
                t = pager.path(a.attr.arrow.path).attr(a.attr.arrow);
                x = new j();
                x.hide();
                f = pager.text(0, 0, a.text.text).attr(a.text);
                m();
                function m() {
                    var r = x.toPathString(), o = x.midDot().pos();
                    i.attr({
                        path: r[0]
                    });
                    t.attr({
                        path: r[1]
                    });
                    f.attr({
                        x: o.x + h.x,
                        y: o.y + h.y
                    })
                }
                this.getId = function () {
                    return g
                };
            };
            diagram.init = function (d, curnode) {
                var z = {};
                if (d) {
                    if (d.data.rects) {
                        for (var s in d.data.rects) {
                            var r;
                            if (d.data.rects[s].data.id == curnode) {
                                r = new diagram.rect(d.data.rects[s], curnode);
                            }
                            else {
                                r = new diagram.rect(d.data.rects[s]);
                            }
                            z[d.data.rects[s].data.id] = r;
                            diagram.rectarr[r.getId()] = r;
                        }
                    }
                    if (d.data.paths) {
                        for (var s in d.data.paths) {
                            var n = new diagram.path(z[d.data.paths[s].from], z[d.data.paths[s].to], d.data.paths[s]);
                            diagram.patharr[n.getId()] = n;
                        }
                    }
                }
                if (curnode != "" && z[curnode]) {
                    $("html").scrollLeft(z[curnode].getBBox().x + z[curnode].getBBox().width / 2 - 325);
                    $("html").scrollTop(z[curnode].getBBox().y + z[curnode].getBBox().height / 2 - 200);
                }
                else {
                    $("html").scrollLeft(200);
                    $("html").scrollTop(100);
                }
            };
            return diagram;
        }
    };
    $.Diagram = Diagram;
})(jQuery);