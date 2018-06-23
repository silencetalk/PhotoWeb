        function ShowDialog(FormID) {
            var dlgform=document.getElementById(FormID);
            $("#FormDivBg").show();
            $(dlgform).show();
        };
        function HideDialog(FormID) {
            var dlgform=document.getElementById(FormID);
            $("#FormDivBg").hide();
            $(dlgform).hide();
        };
        function Print_Click() {
            bdhtml = window.document.body.innerHTML;
            sprnstr = "<!--startprint-->";
            eprnstr = "<!--endprint-->";
            prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
            window.document.body.innerHTML = prnhtml;
            window.print();
        };
        function ImgZoom(obj) {
            var src=$(obj).attr("src");
            $("#zoomimg").attr("src",src);
//          var screenheight=document.body.clientHeight-90;
            var screenheight=window.screen.availHeight-90;
//            var screenwidth=document.body.clientWidth;
            var scrollheight=document.body.scrollTop;
            var top=0,left="317px";
            if(screenheight>440)
            {
                top=(screenheight-440)/2+scrollheight-90+"px";
            }
//            if(screenwidth>500)
//            {
//                left=(screenwidth-500)/2+"px";
//            }
            $(".zoomimg").css({"top":top,"left":left});
            $("#FormDivBg").show();
            $("#imgdiv").show();
        };
        function ShowNews(){
              var width=document.body.offsetWidth;
              var left=(width-300)/2;
              document.getElementById("News").style.left=left+"px";
              $("#News").show("slow");
        };