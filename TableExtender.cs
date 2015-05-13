using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TXTextControl;

namespace TXTextControl
{
    public static class TableExtender
    {
        public static void AutoSize(this Table table, ServerTextControl TextControl)
        {
            int[] iColWidths = new int[table.Columns.Count];

            foreach (TXTextControl.TableCell tc in table.Cells)
            {
                int textBounds = 0;

                // check the width of every line in a cell
                for (int i = tc.Start; i <= tc.Start + tc.Length - 1; i++)
                {
                    // pick width, if the current one is the longest
                    if (TextControl.Lines.GetItem(i).TextBounds.Width > textBounds)
                    {
                        textBounds = TextControl.Lines.GetItem(i).TextBounds.Width;
                    }
                }

                // pick the width, if it is the longest in the whole column
                if (textBounds > (int)iColWidths.GetValue(tc.Column - 1))
                {
                    iColWidths.SetValue(textBounds, tc.Column - 1);
                }
            }

            // resize the table due to the filled array
            for (int iColCount = 1; iColCount <= table.Columns.Count; iColCount++)
            {
                if ((int)iColWidths.GetValue(iColCount - 1) == 0)
                {
                }
                else
                {
                    table.Columns.GetItem(iColCount).Width = (int)iColWidths.GetValue(iColCount - 1) +
                        table.Columns.GetItem(iColCount).CellFormat.RightTextDistance +
                        table.Columns.GetItem(iColCount).CellFormat.LeftTextDistance +
                        table.Columns.GetItem(iColCount).CellFormat.RightBorderWidth +
                        table.Columns.GetItem(iColCount).CellFormat.LeftBorderWidth;
                }
            }
        }
    }
}
