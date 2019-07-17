
/*js 队列*/
var Qu = {};

//构造函数
Qu.Queue = function (len) {

    this.capacity = len;        //队列最大容量

    this.list = new Array();    //队列数据

};

//入队
Qu.Queue.prototype.enqueue = function (data) {

    if (data == null) return;

    if (this.list.length >= this.capacity) {

        this.list.remove(0);

    }

    this.list.push(data);

};

//出队
Qu.Queue.prototype.dequeue = function () {

    if (this.list == null) return;

    //this.list.remove(0);
    return this.list.dequeue(0);
};

//队列长度
Qu.Queue.prototype.size = function () {

    if (this == null) return;

    return this.list.length;

};

//队列是否空
Qu.Queue.prototype.isEmpty = function () {

    if (this == null || this.list == null) return false;

    return this.list.length < 1;

};

//对象数组扩展remove
Array.prototype.remove = function (dx) {
    if (isNaN(dx) || dx > this.length) {

        return false;
    }

    for (var i = 0, n = 0; i < this.length; i++) {

        if (this[i] != this[dx]) {

            this[n++] = this[i];
        }

    }

    this.length -= 1

}

//对象数组扩展dequeue
Array.prototype.dequeue = function (dx) {

    var s = this[dx];

    if (isNaN(dx) || dx > this.length) {
        return false;
    }

    for (var i = 0, n = 0; i < this.length; i++) {

        if (this[i] != this[dx]) {

            this[n++] = this[i];
        }

    }

    this.length -= 1

    return s;
}
