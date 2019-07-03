function getPos(Gr, Hu, Ra, Sl, House_Id) {
    var points_Array = [Gr, Hu, Ra, Sl];
    for (x = 0; x < points_Array.length; x++) {
        position_int = 1;
        position_from_first = 0;
        for (i = 0; i <= 3; i++) {
            if (points_Array[x] < points_Array[i] && x != i) {
                position_from_first++;
            }
        }
        position_int += position_from_first;
        switch (x) {
            case 0:
                switch (position_int) {
                    case 1:
                        Gr_Pos = '1st';
                        break;
                    case 2:
                        Gr_Pos = '2nd';
                        break;
                    case 3:
                        Gr_Pos = '3rd';
                        break;
                    case 4:
                        Gr_Pos = '4th';
                        break;
                }
                break;
            case 1:
                switch (position_int) {
                    case 1:
                        Hu_Pos = '1st';
                        break;
                    case 2:
                        Hu_Pos = '2nd';
                        break;
                    case 3:
                        Hu_Pos = '3rd';
                        break;
                    case 4:
                        Hu_Pos = '4th';
                        break;
                }
                break;
            case 2:
                switch (position_int) {
                    case 1:
                        Ra_Pos = '1st';
                        break;
                    case 2:
                        Ra_Pos = '2nd';
                        break;
                    case 3:
                        Ra_Pos = '3rd';
                        break;
                    case 4:
                        Ra_Pos = '4th';
                        break;
                }
                break;
            case 3:
                switch (position_int) {
                    case 1:
                        Sl_Pos = '1st';
                        break;
                    case 2:
                        Sl_Pos = '2nd';
                        break;
                    case 3:
                        Sl_Pos = '3rd';
                        break;
                    case 4:
                        Sl_Pos = '4th';
                        break;
                }
                break;
        }
    }
    var position_Array = [Gr_Pos, Hu_Pos, Ra_Pos, Sl_Pos];
    document.getElementById("position").innerHTML = position_Array[House_Id];
}