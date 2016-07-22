;(function(){
	var LightBox = function(){
		var self = this;

		// create mask and popup window.
		this.popupMask = $('<div id="G-lightbox-mask">');
		this.popupWin = $('<div id="G-lightbox-popup">');

		// save body
		this.bodyNode = $(document.body);

		// render others DOM and inset into body.
		this.rederDOM();

		this.picViewArea = this.popupWin.find("div.lightbox-pic-view"); // 
		this.popupPic = this.popupWin.find("img.lightbox-image"); // image
		this.picCaptionArea = this.popupWin.find("div.lightbox-caption-area");
		this.nextBtn = this.popupWin.find("span.lightbox-next-btn");
		this.preBtn = this.popupWin.find("span.lightbox-prev-btn");

		this.captionText = this.popupWin.find("p.lightbox-pic-desc");
		this.currentIndex = this.popupWin.find("span.lightbox-of-index");
		this.closeBtn = this.popupWin.find("span.lightbox-close-btn");

		// event
		this.groupName = null;
		this.groupData = [];
		this.bodyNode.delegate(".js-lightbox,*[data-role=lightbox]","click",function(e){
			// 
			e.stopPropagation();

			// get groupName
			var currentGroupName = $(this).attr("data-group");

			if (currentGroupName != self.groupName) {
				self.groupName = currentGroupName;
				//get all group data depend on data-group
				self.getGroup();
			};

			// initial popup Wndows.
			self.initPopup($(this));

		});

		//close
		this.popupMask.click(function(){
			$(this).fadeOut();
			self.popupWin.fadeOut();
		});

		//close btn
		this.closeBtn.click(function(){
			self.popupMask.fadeOut();
			self.popupWin.fadeOut();
		});

		//
		this.flag = true;

		//
		this.nextBtn.hover(function(){
			if (!$(this).hasClass("disabled") && self.groupData.length > 1) {
				$(this).addClass("lightbox-next-btn-show");
			}
		},function(){
			if (!$(this).hasClass("disabled") && self.groupData.length > 1) {
					$(this).removeClass("lightbox-next-btn-show");
				}
		}).click(function(e){

			if (!$(this).hasClass("disabled") && self.flag){
				self.flag = false;
				e.stopPropagation();
				self.goto("next");
			}
		});

		//
		this.preBtn.hover(function(){
			if (!$(this).hasClass("disabled") && self.groupData.length > 1) {
				$(this).addClass("lightbox-prev-btn-show");
			}
		},function(){
			if (!$(this).hasClass("disabled") && self.groupData.length > 1) {
					$(this).removeClass("lightbox-prev-btn-show");
				}
		}).click(function(e){
			if (!$(this).hasClass("disabled") && self.flag){
				self.flag = false;
				e.stopPropagation();
				self.goto("prev");
			}
		});
	}

	LightBox.prototype={
		goto:function(dir){
			if (dir === "next") {
				//
				this.index++;
				if (this.index >= this.groupData.length - 1) {
					this.nextBtn.addClass("disabled").removeClass("lightbox-next-btn-show");
				}
				if (this.index != 0) {
					this.preBtn.removeClass("disabled");
				}

				var src = this.groupData[this.index].src;
				this.loadPicSize(src);
				//this.groupData;


			}else if(dir === "prev"){
				this.index--;

				if (this.index <= 0) {
					this.preBtn.addClass("disabled").removeClass("lightbox-prev-btn-show");
				}
				if (this.index != this.groupData.length - 1) {
					this.nextBtn.removeClass("disabled");
				}

				var src = this.groupData[this.index].src;
				this.loadPicSize(src);
			}
		},
		loadPicSize:function(sourceSrc){
			var self = this;

			self.popupPic.css({
				width: "auto",
				height: "suto"
			}).hide();

			this.preLoadImg(sourceSrc, function(){	

				self.popupPic.attr("src", sourceSrc);
				var picWidth = self.popupPic.width(),
					picHeight = self.popupPic.height();

				self.changePic(picWidth, picHeight);

			});
		},
		changePic : function(picWidth, picHeight){
			var self = this,
				winWidth = $(window).width(),
				winHeight = $(window).height();

				//
				var scale = Math.min(winWidth/(picWidth+10),picHeight/(picHeight + 10), 1);
			picWidth = picWidth * scale;
			picHeight = picHeight * scale;

			console.log(winHeight);
			console.log(picHeight);
			console.log((winHeight - picHeight) / 2);

			this.picViewArea.animate({
				wdith: picWidth - 10,
				height: picHeight - 10
			});
			this.popupWin.animate({
				width : picWidth,
				height : picHeight,
				marginLeft: -(picWidth / 2),
				top: (winHeight - picHeight) / 2
			}, function(){
				self.popupPic.css({
					width : picWidth - 10,
					height : picHeight - 10
				}).fadeIn();

				self.picCaptionArea.fadeIn();
				self.closeBtn.fadeIn();
				self.flag = true;
			});

			//


			this.captionText.text(this.groupData[this.index].caption);
			this.currentIndex.text("Current index: "+ (this.index + 1)+" of " + this.groupData.length);
		},
		preLoadImg : function(sourceSrc, callback){
			var img = new Image();

			if (!!window.activeXObject) {
				img.onreadystatechange = function(){
					if (this.readyState == "complete") {
						callback();
					};
				};
			}
			else
			{
				img.onload	= function(){
					callback();
				}
			}

			img.src = sourceSrc;
		},
		getIndexOf : function(currentId){
			var index = 0;

			$(this.groupData).each(function(i){
				index = i;

				if (this.id == currentId) {
					return false;
				}
			});

			return index;
		},
		showMaskAndPopup : function(sourceSrc, currentId){
			var self = this;

			this.popupPic.hide();
			this.picCaptionArea.hide();
			this.closeBtn.hide();
 
			this.popupMask.fadeIn();

			var winWidth = $(window).width(),
			winHeight = $(window).height();

			/*
			this.picViewArea.css({
				width : winWidth/2,
				height : winHeight/2
			});
			*/

			this.popupWin.fadeIn();

			var viewHeight = winHeight/2 + 10;


			this.popupWin.css({
				width : winWidth/2 + 10,
				height : winHeight/2 + 10,
				marginLeft:-(winWidth/2 + 10)/2,
				top: -viewHeight
			}).animate({
				top: (winHeight - viewHeight) /2
			}, function(){
				// load image.
				self.loadPicSize(sourceSrc);
			});


			// 
			this.index = this.getIndexOf(currentId);

			var groupDataLength = this.groupData.length;
			if (groupDataLength > 1) {
				if (this.index === 0) {
					this.preBtn.addClass("disabled");
					this.nextBtn.removeClass("disabled");
				}
				else if (this.index === groupDataLength - 1)
				{
					this.preBtn.removeClass("disabled");
					this.nextBtn.addClass("disabled");
				}
				else
				{
					this.preBtn.removeClass("disabled");
					this.nextBtn.removeClass("disabled");
				}
			}
		},
		initPopup :function(currentObj){
			var self = this,
				sourceSrc = currentObj.attr("data-source"),
				currentId = currentObj.attr("data-id");

			this.showMaskAndPopup(sourceSrc, currentId);
		},
		getGroup : function(){
			var self = this;

			// get the same group data.
			var groupList = this.bodyNode.find('*[data-group=' + this.groupName + ']');

			self.groupData.length = 0;

			groupList.each(function(){
				self.groupData.push({
					src : $(this).attr("data-source"),
					id : $(this).attr("data-id"),
					caption: $(this).attr("data-caption")
				});
			});
		},

		rederDOM : function(){
			var strDom = '<div class="lightbox-pic-view">' +
						  '<span class="lightbox-btn lightbox-prev-btn"></span>' +
						  '<img class="lightbox-image">' +
						  '<span class="lightbox-btn lightbox-next-btn"></span>' +
						  '</div>' +
						  '<div class="lightbox-pic-caption">' +
					      '<div class="lightbox-caption-area">' +
						  '<p class="lightbox-pic-desc"></p>' +
						  '<span class="lightbox-of-index"></span>' +
						  '</div>' +
						  '<span class="lightbox-close-btn"></span>' +
						  '</div>';

			// insert into this.popupWin
			this.popupWin.html(strDom);

			// 
			this.bodyNode.append(this.popupMask, this.popupWin);
		}
	};

	window["LightBox"] = LightBox;
})();